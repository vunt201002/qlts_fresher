using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.DL.Base;

namespace Misa.Qlts.Solution.DL.Entities
{
    /// <summary>
    /// Lớp người dùng
    /// </summary>
    /// created by: ntvu (10/07/2023)
    public class User : BaseEntity
    {
        public string email { get; set; }
        public byte[] password_salt { get; set; }
        public byte[] password_hash { get; set; }
    }
}
