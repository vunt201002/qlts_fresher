using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Misa.Qlts.Solution.DL.Context
{
    public class DapperContext
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        #endregion

        #region Constructor
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MyConnection");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo một kết nối đến database.
        /// </summary>
        /// <returns>IDbConnection</returns>
        /// Created by: ntvu (12/05/2023)
        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString); 
        #endregion
    }
}
