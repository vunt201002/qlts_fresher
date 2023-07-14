using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.AuthService;
using Misa.Qlts.Solution.BL.AuthService.AuthDtos;
using Misa.Qlts.Solution.Common.CommonEntities;
using Misa.Qlts.Solution.Controller.Base;
using Misa.Qlts.Solution.DL.Contracts;

namespace Misa.Qlts.Solution.Controller.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController<AuthDto, AuthCreateDto, AuthUpdateDto>
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) : base(authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// hàm login
        /// </summary>
        /// <param name="authDto"></param>
        /// <returns>Task<IActionResult></returns>
        /// created by: ntvu (11/07/2023)
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDto authDto)
        {
            // login
            var res = await _authService.Login(authDto);

            // tạo refresh token
            RefreshToken refreshToken = _authService.GetRefreshToken();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            return Ok(res);
        }

        [HttpPost("otp")]
        public async Task<IActionResult> GetOTP(string email)
        {
            var otp = await _authService.GetOTP(email);

            return Ok(otp);
        }

        /// <summary>
        /// hàm verify otp
        /// </summary>
        /// <param name="otp"></param>
        /// <returns>Task<IActionResult></returns>
        /// created by: ntvu (13/07/2023)
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(OTP otp)
        {
            var user = await _authService.VerifyOtp(otp);

            return Ok(user);
        }

        [HttpGet("test"), Authorize(Roles = "Admin")]
        public string? TestController()
        {
            // get user role
            var rs = User.FindFirstValue(ClaimTypes.Email);
            return rs;
        }
    }
}
