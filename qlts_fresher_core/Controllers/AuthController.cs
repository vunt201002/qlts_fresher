using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.AuthService;
using Misa.Qlts.Solution.BL.AuthService.AuthDtos;
using Misa.Qlts.Solution.Controller.Base;

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
            var res = await _authService.Login(authDto);

            return Ok(res);
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
