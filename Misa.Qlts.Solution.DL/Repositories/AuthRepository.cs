using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Misa.Qlts.Solution.DL.Base;
using Misa.Qlts.Solution.DL.Context;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;
using Misa.Qlts.Solution.Common.CommonEntities;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Misa.Qlts.Solution.DL.Repositories
{
    /// <summary>
    /// Interface người dùng
    /// </summary>
    /// created by: ntvu (10/07/2023)
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public AuthRepository(DapperContext context) : base(context)
        {
            
        }


        /// <summary>
        /// hàm check email tồn tại
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Task<User></returns>
        /// created by: ntvu (11/07/2023)
        public async Task<User> CheckEmailExit(string email)
        {
            var procedureName = "sp_check_email_exist";

            using(var connection = _context.CreateConnection())
            {
                var userDoc = await connection.QueryFirstOrDefaultAsync<User>(
                    procedureName,
                    new {
                        email
                    },
                    commandType : CommandType.StoredProcedure
                );

                return userDoc;
            }
        }

        /// <summary>
        /// hàm cập nhật otp
        /// </summary>
        /// <param name="email"></param>
        /// <param name="otp"></param>
        /// <returns>Task<int></returns>
        /// created by: ntvu (13/07/2023)
        public async Task<int> UpdateOtp(string email, OTP otp)
        {
            var procedureName = "sp_update_user_otp";

            var parameters = new DynamicParameters();

            parameters.Add("email", email);
            parameters.Add("otp", otp.otp);
            parameters.Add("otp_expiry_time", otp.otp_expiry_time);

            using (var connection = _context.CreateConnection())
            {
                int res = await connection.ExecuteAsync(
                    procedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

        /// <summary>
        /// hàm verify otp
        /// </summary>
        /// <param name="otp"></param>
        /// <returns>Task<OTP></returns>
        /// created by: ntvu (13/07/2023)
        public async Task<User> VerifyOtp(OTP otp)
        {
            var procedureName = "sp_verify_otp";

            using (var connection = _context.CreateConnection())
            {
                var res = await connection.QueryFirstOrDefaultAsync<User>(
                    procedureName,
                    otp,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

        /// <summary>
        /// hàm verify người dùng
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Task<int></returns>
        /// created by: ntvu (13/07/2023)
        public async Task<int> VerifyUser(string email)
        {
            var procedureName = "sp_verify_user";

            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("email", email);

            using (var connection = _context.CreateConnection())
            {
                int res = await connection.ExecuteAsync(
                    procedureName,
                    dynamicParam,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

    }
}
