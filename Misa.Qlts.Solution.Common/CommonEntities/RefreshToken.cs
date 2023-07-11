using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.CommonEntities
{
    /// <summary>
    /// refresh token
    /// </summary>
    /// created by: ntvu (11/07/2023)
    public class RefreshToken
    {
        public string Token { get; set; }           // token
        public DateTime Created { get; set; }       // ngày tạo
        public DateTime Expires { get; set; }       // ngày hết hạn
    }
}
