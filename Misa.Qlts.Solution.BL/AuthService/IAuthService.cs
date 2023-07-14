using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.BL.AuthService.AuthDtos;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.Common.CommonEntities;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.AuthService
{
    public interface IAuthService : IBaseService<AuthDto, AuthCreateDto, AuthUpdateDto>
    {
        /// <summary>
        /// hàm gọi tới repo, đăng nhập
        /// </summary>
        /// <param name="authDto"></param>
        /// <returns>Task<string></returns>
        /// created by: ntvu (11/07/2023)
        public Task<string> Login(AuthDto authDto);

        /// <summary>
        /// hàm get refresh token,
        /// để bên ngoài lấy token
        /// </summary>
        /// <returns>RefreshToken</returns>
        /// created by: ntvu (11/07/2023)
        public RefreshToken GetRefreshToken();

        /// <summary>
        /// lấy otp
        /// </summary>
        /// <returns>OTP</returns>
        /// created by: ntvu (13/07/2023)
        public Task<OTP> GetOTP(string email);

        /// <summary>
        /// hàm verify otp
        /// </summary>
        /// <param name="otp"></param>
        /// <returns>Task<User></returns>
        /// created by: ntvu (13/07/2023)
        public Task<User> VerifyOtp(OTP otp);
    }
}
