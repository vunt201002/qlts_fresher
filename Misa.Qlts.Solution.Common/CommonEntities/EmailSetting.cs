using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.CommonEntities
{
    /// <summary>
    /// lớp chứa các setting của
    /// mail server
    /// </summary>
    /// created by: ntvu (14/07/2023)
    public class EmailSetting
    {
        public string Email { get; set; }           // email
        public string Password { get; set; }        // mật khẩu
        public string Host { get; set; }            // link host
        public string DisplayName { get; set; }     // tên hiển thị
        public int Port { get; set; }               // cổng
    }
}
