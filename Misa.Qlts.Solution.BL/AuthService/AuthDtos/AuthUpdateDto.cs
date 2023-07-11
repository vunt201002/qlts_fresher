using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.BL.AuthService.AuthDtos
{
    /// <summary>
    /// lớp chỉnh sửa người dùng
    /// </summary>
    public class AuthUpdateDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MinLength(8)]
        public string password { get; set; }
    }
}
