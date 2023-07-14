using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Misa.Qlts.Solution.DL.Context;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.DL.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Properties
        protected readonly DapperContext _context;
        #endregion

        #region Constructor
        public BaseRepository(DapperContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm chuyển tên bảng từ PascalCase sang snake_case.
        /// </summary>
        /// <param name="pascalCase">Tên bảng ở dạng PascalCase</param>
        /// <returns>string</returns>
        /// Created by: ntvu (19/05/2023)
        public static string ConverPascalToSnakeCase(string pascalCase)
        {
            string snakeCase = Regex.Replace(
                pascalCase,
                @"(\p{Ll})(\p{Lu})",
                "$1_$2"
            ).ToLower();

            return snakeCase;
        }

        /// <summary>
        /// Hàm thêm một bản ghi.
        /// </summary>
        /// <param name="entity">Giá trị bản ghi</param>
        /// <returns></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var snakeCaseTableName = ConverPascalToSnakeCase(tableName);

            var procedureName = $"sp_add_{snakeCaseTableName}";

            using (var connection = _context.CreateConnection())
            {
                int res = await connection.ExecuteAsync(
                    procedureName,
                    entity,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

        /// <summary>
        /// Hàm xóa (một hoặc nhiều) bản ghi.
        /// </summary>
        /// <param name="Ids">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<int> DeleteAsync(object Ids)
        {
            var tableName = typeof(TEntity).Name;
            var snakeCaseTableName = ConverPascalToSnakeCase(tableName);

            var procedureName = $"sp_delete_{snakeCaseTableName}s";

            using (var connection = _context.CreateConnection())
            {
                int res = await connection.ExecuteAsync(
                    procedureName,
                    Ids,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

        /// <summary>
        /// Hàm lấy một bản ghi.
        /// </summary>
        /// <param name="Id">Id của bản ghi</param>
        /// <returns>TEntity</returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<TEntity> GetAsync(Guid Id)
        {
            var tableName = typeof(TEntity).Name;
            var snakeCaseTableName = ConverPascalToSnakeCase(tableName);
            var procedureName = $"sp_get_{snakeCaseTableName}";
            var dynamicParams = new DynamicParameters();

            dynamicParams.Add($"{tableName}Id", Id);

            using (var connection = _context.CreateConnection())
            {
                var entity = await connection.QuerySingleOrDefaultAsync<TEntity>(
                    procedureName,
                    dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return entity;
            }
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi.
        /// </summary>
        /// <returns>IEnumerable<TEntity></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<IEnumerable<TEntity>> GetListAsync()
        {
            var tableName = typeof(TEntity).Name;
            var snakeCaseTableName = ConverPascalToSnakeCase(tableName);
            var procedureName = $"sp_get_{snakeCaseTableName}s";

            using (var connection = _context.CreateConnection())
            {
                var entities = await connection.QueryAsync<TEntity>(
                    procedureName,
                    commandType: CommandType.StoredProcedure
                );

                return entities.ToList();
            }
        }

        /// <summary>
        /// Hàm chỉnh sửa bản ghi.
        /// </summary>
        /// <param name="Id">Id bản ghi</param>
        /// <param name="entity">Thông tin chỉnh sửa</param>
        /// <returns></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<int> UpdateAsync(Guid Id, TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var snakeCaseTableName = ConverPascalToSnakeCase(tableName);
            var procedureName = $"sp_update_{snakeCaseTableName}";

            using (var connection = _context.CreateConnection())
            {
                int res = await connection.ExecuteAsync(
                    procedureName,
                    entity,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }
        #endregion
    }
}
