using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Misa.Qlts.Solution.Common.CommonEntities;
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

        /// <summary>
        /// hàm cập nhật otp
        /// </summary>
        /// <param name="email"></param>
        /// <param name="otp"></param>
        /// <returns>Task<int></returns>
        /// created by: ntvu (13/07/2023)
        public Task<int> UpdateOtp(string email, OTP otp);

        /// <summary>
        /// hàm verify otp
        /// </summary>
        /// <param name="otp"></param>
        /// <returns>Task<OTP></returns>
        /// created by: ntvu (13/07/2023)
        public Task<User> VerifyOtp(OTP otp);

        /// <summary>
        /// hàm verify người dùng
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Task<int></returns>
        /// created by: ntvu (13/07/2023)
        public Task<int> VerifyUser(string email);

    }
}
