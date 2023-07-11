using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.DL.Base;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.DL.Contracts
{
    /// <summary>
    /// Interface người dùng
    /// </summary>
    /// created by: ntvu (10/07/2023)
    public interface IAuthRepository : IBaseRepository<User>
    {
        /// register là add user, kế thừa từ base


        /// <summary>
        /// hàm check email tồn tại
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Task<User></returns>
        /// created by: ntvu (11/07/2023)
        public Task<User> CheckEmailExit(string email);

    }
}
