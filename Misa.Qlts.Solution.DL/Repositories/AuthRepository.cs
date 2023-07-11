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

    }
}
