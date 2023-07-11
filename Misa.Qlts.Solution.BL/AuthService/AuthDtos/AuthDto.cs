using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.BL.AuthService.AuthDtos
{
    /// <summary>
    /// Lớp nhận người dùng
    /// </summary>
    /// created by: ntvu (10/07/2023)
    public class AuthDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MinLength(8)]
        public string password { get; set; }
    }
}
