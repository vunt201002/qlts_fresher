using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Misa.Qlts.Solution.BL.AuthService.AuthDtos;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;
using Misa.Qlts.Solution.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Misa.Qlts.Solution.BL.AuthService
{
    /// <summary>
    /// service người dùng
    /// </summary>
    /// created by: ntvu (10/07/2023)
    public class AuthService : BaseService<User, AuthDto, AuthCreateDto, AuthUpdateDto>, IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(
            IAuthRepository authRepository,
            IMapper mapper
        ) : base(authRepository, mapper)
        {
            _authRepository = authRepository;
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

            var token = CreateToken(emailExist);

            return token;
        }

    }
}
