using System.Security.Cryptography;
using AutoMapper;
using Misa.Qlts.Solution.BL.AuthService.AuthDtos;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;
using Misa.Qlts.Solution.Common.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Misa.Qlts.Solution.Common.CommonEntities;
using Microsoft.AspNetCore.Http;
using Misa.Qlts.Solution.BL.MailService;

namespace Misa.Qlts.Solution.BL.AuthService
{
    /// <summary>
    /// service người dùng
    /// </summary>
    /// created by: ntvu (10/07/2023)
    public class AuthService : BaseService<User, AuthDto, AuthCreateDto, AuthUpdateDto>, IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMailService _mailService;

        public AuthService(
            IAuthRepository authRepository,
            IMapper mapper,
            IMailService mailService
        ) : base(authRepository, mapper)
        {
            _authRepository = authRepository;
            _mailService = mailService;
        }

        /// <summary>
        /// hàm tạo password hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// created by: ntvu (10/07/2023)
        private static void CreateHashPassword(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        )
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        /// <summary>
        /// hàm so sánh mật khẩu
        /// được hash
        /// </summary>
        /// <param name="password">mật khẩu</param>
        /// <param name="passwordHash">mật khẩu</param>
        /// <returns>book</returns>
        /// created by: ntvu (11/07/2023)
        private static bool VerifyPasswordHash(
            string password,
            byte[] password_hash,
            byte[] password_salt
        )
        {
            using (var hmac = new HMACSHA512(password_salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(password_hash);
            }
        }


        /// <summary>
        /// hàm tạo accessToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        /// created by: ntvu (11/07/2023)
        private static string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                "my top secret keymy top secret keymy top secret keymy top secret key"
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        /// <summary>
        /// hàm tạo refresh token,
        /// chỉ dùng trong service
        /// </summary>
        /// <returns>RefreshToken</returns>
        /// created by: ntvu (11/07/2023)
        private static RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(7)
            };

            return refreshToken;
        }

        /// <summary>
        /// hàm get refresh token,
        /// để bên ngoài lấy token
        /// </summary>
        /// <returns>RefreshToken</returns>
        /// created by: ntvu (11/07/2023)
        public RefreshToken GetRefreshToken()
        {
            return GenerateRefreshToken();
        }

        /// <summary>
        /// hàm tạo otp
        /// </summary>
        /// <returns>string</returns>
        /// created by: ntvu (13/07/2023)
        private static OTP GenerateOtp()
        {
            Random random = new Random();
            int otpNumber = random.Next(100000, 999999);

            // tạo otp
            string otpString = otpNumber.ToString();
            // thời gian hết hạn (10 phút)
            DateTime expirationTime = DateTime.Now.AddMinutes(10);

            OTP oTP = new OTP(otpString, expirationTime);

            return oTP;
        }

        /// <summary>
        /// lấy otp
        /// </summary>
        /// <returns>OTP</returns>
        /// created by: ntvu (13/07/2023)
        public async Task<OTP> GetOTP(string email)
        {
            // get new otp
            var newOtp = GenerateOtp();

            // save otp to db
            await _authRepository.UpdateOtp(email, newOtp);

            return newOtp;
        }

        /// <summary>
        /// hàm verify otp
        /// </summary>
        /// <param name="otp"></param>
        /// <returns>Task<User></returns>
        /// created by: ntvu (13/07/2023)
        public async Task<User> VerifyOtp(OTP otp)
        {
            var user = await _authRepository.VerifyOtp(otp);

            // nếu user null, verify lỗi
            if (user == null)
            {
                throw new BadRequestException("Lỗi khi verify otp");
            }
            // nếu có user, verify thành công
            // update trạng thái user
            else
            {
                await _authRepository.VerifyUser(user.email);
            }

            return user;
        }

        /// <summary>
        /// hàm đăng ký người dùng,
        /// override từ hàm thêm bản
        /// ghi.
        /// </summary>
        /// <param name="authCreateDto"></param>
        /// <returns>Task<int></returns>
        /// <exception cref="BadRequestException">lỗi input</exception>
        /// created by: ntvu (11/07/2023)
        public override async Task<int> AddAsync(AuthCreateDto authCreateDto)
        {
            // kiểm tra email tồn tại
            var emailExist = await _authRepository.CheckEmailExit(authCreateDto.email);

            if (emailExist != null)
            {
                throw new BadRequestException("Email đã được đăng ký");
            }

            // tạo hash password
            CreateHashPassword(
                authCreateDto.password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );

            // người dùng mới
            User newUser = new()
            {
                email = authCreateDto.email,
                password_hash = passwordHash,
                password_salt = passwordSalt
            };

            // validate dữ liệu trước khi truyền xuống cho DL


            // gọi tới repo tạo người dùng mới
            int res = await _authRepository.AddAsync( newUser );

            // nếu tạo không thành công
            if (res == 0 )
            {
                throw new BadRequestException();
            }

            // nếu tạo thành công thì tạo otp
            OTP otp = GenerateOtp();

            // gửi mail cho người dùng
            EmailRequest otpMail = new()
            {
                ToEmail = authCreateDto.email,
                Subject = "Xác thực tài khoản",
                Body = $"Otp của bạn là: { otp.otp }"
            };

            _mailService.SendEmailAsync(otpMail);

            // lưu otp
            await _authRepository.UpdateOtp(authCreateDto.email, otp);
            
            return res;
        }

        /// <summary>
        /// hàm gọi tới repo, đăng nhập
        /// </summary>
        /// <param name="authDto"></param>
        /// <returns>Task<string></returns>
        /// created by: ntvu (11/07/2023)
        public async Task<string> Login(AuthDto authDto)
        {
            // kiểm tra email tồn tại
            var emailExist = await _authRepository.CheckEmailExit(authDto.email);

            if (emailExist == null)
            {
                throw new BadRequestException("Email chưa được đăng ký");
            }

            var verifyPassword = VerifyPasswordHash(
                authDto.password,
                emailExist.password_hash,
                emailExist.password_salt);

            if (!verifyPassword)
            {
                throw new BadRequestException("Sai mật khẩu");
            }

            // tạo access token
            var token = CreateToken(emailExist);

            return token;
        }


        /// <summary>
        /// hàm override từ base repo
        /// thay đổi mật khẩu
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="authUpdateDto"></param>
        /// <returns>Task<int></returns>
        /// <exception cref="BadRequestException"></exception>
        /// created by: ntvu (13/07/2023)
        public override async Task<int> UpdateAsync(Guid Id, AuthUpdateDto authUpdateDto)
        {
            // hash password mới
            CreateHashPassword(
                authUpdateDto.password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );

            // tạo user mới với mật khẩu mới
            User newUser = new()
            {
                email = authUpdateDto.email,
                password_salt = passwordSalt,
                password_hash = passwordHash,
            };

            // gửi link reset qua email
            EmailRequest otpMail = new()
            {
                ToEmail = authUpdateDto.email,
                Subject = "Đổi mật khẩu",
                Body = $"Link đổi mật khẩu: http://localhost:44327/api/auth/reset-password"
            };

            _mailService.SendEmailAsync(otpMail);


            // Chuyển dữ liệu cho DL để cập nhật
            int res = await _authRepository.UpdateAsync(Id, newUser);

            if (res != 1)
            {
                throw new BadRequestException("Lỗi khi đổi mật khẩu");
            }

            return res;
        }
    }
}
