using Dapper;
using Misa.Qlts.Solution.DL.Base;
using Misa.Qlts.Solution.DL.Context;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;
using System.Data;

namespace Misa.Qlts.Solution.DL.Repositories
{
    public class FixedAssetCategoryRepository : BaseRepository<FixedAssetCategory>, IFixedAssetCategoryRepository
    {
        #region Constructor
        public FixedAssetCategoryRepository(DapperContext context) : base(context)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả các loại tài sản.
        /// </summary>
        /// <returns>IEnumerable<FixedAssetCategory></returns>
        /// Created by: ntvu (12/05/2023)
        public override async Task<IEnumerable<FixedAssetCategory>> GetListAsync()
        {
            var procedureName = "sp_get_fixed_asset_categories";

            using (var connection = _context.CreateConnection())
            {
                var departments = await connection.QueryAsync<FixedAssetCategory>(
                    procedureName,
                    commandType: CommandType.StoredProcedure
                );

                return departments.ToList();
            }
        } 
        #endregion
    }
}
