using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.Common.CommonEntites;
using Misa.Qlts.Solution.Common.CommonEntities;
using Misa.Qlts.Solution.DL.Base;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.DL.Contracts
{
    public interface IFixedAssetRepository : IBaseRepository<FixedAsset>
    {
        #region Methods
        /// <summary>
        /// Hàm lấy tất cả tài sản và phân trang.
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <returns>IEnumerable<FixedAsset></returns>
        /// Created by: ntvu (12/05/2023)
        public Task<IEnumerable<FixedAsset>> GetFixedAssetsAsync(LimitPage limit);

        /// <summary>
        /// Lọc và phân trang tài sản.
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>IEnumerable<FixedAsset></returns>
        /// Created by: ntvu (12/05/2023)
        public Task<IEnumerable<FixedAsset>> FilteringAndPagingAsync(
            LimitPage limit,
            FilterModel filter
        );

        /// <summary>
        /// Lấy id của tài sản.
        /// </summary>
        /// <param name="fixedAssetCode">mã tài sản</param>
        /// <returns>Guid</returns>
        /// Created by: ntvu (23/05/2023)
        public Task<Guid> GetFixedAssetIdAsync(string fixedAssetCode);

        /// <summary>
        /// Hàm check trùng mã
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <param name="fixedAssetCode">Mã tài sản cần check</param>
        /// <returns>int</returns>
        /// created by: ntvu (28/05/2023)
        public Task<int> CheckDuplicateCodeAsync(Guid fixedAssetId, string fixedAssetCode);

        /// <summary>
        /// Hàm tạo mã mới
        /// </summary>
        /// <returns>string</returns>
        /// created by: ntvu (28/05/2023)
        public Task<string> GenerateNewCodeAsync();

        /// <summary>
        /// Hàm lấy summary
        /// </summary>
        /// <returns>Task<Summary></returns>
        /// created by: ntvu (06/06/2023)
        public Task<Summary> GetSummary();

        /// <summary>
        /// Hàm export
        /// </summary>
        /// <returns>Task<FileResult></returns>
        /// created by: ntvu (12/06/2023)
        public Task<FileContentResult> ExportFixedAssetToExcel();

        /// <summary>
        /// hàm import file excel thành object
        /// </summary>
        /// <returns>Task<IEnumerable<FixedAssetImport>></returns>
        /// created by: ntvu (20/06/2023)
        public List<FixedAssetImport> ImportExcel<FixedAssetImport>(ImportModel importModel);
        #endregion
    }
}
