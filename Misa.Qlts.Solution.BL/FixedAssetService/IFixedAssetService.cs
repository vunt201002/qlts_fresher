
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos;
using Misa.Qlts.Solution.Common.CommonEntites;
using Misa.Qlts.Solution.Common.CommonEntities;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.FixedAssetService
{
    public interface IFixedAssetService : IBaseService<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        #region Methods
        /// <summary>
        /// Hàm gọi tới repo, lấy tất cả tài sản
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <returns>IEnumerable<FixedAssetDto></returns>
        /// Created by: ntvu (19/05/2023)
        public Task<IEnumerable<FixedAssetDto>> GetFixedAssetsAsync(LimitPage limit);

        /// <summary>
        /// Hàm gọi tới repo, lọc và phân trang tài sản
        /// </summary>
        /// <param name="limit">số trang và số bản ghi mỗi trang</param>
        /// <param name="filter">điều kiện lọc</param>
        /// <returns>IEnumerable<FixedAssetDto></returns>
        /// Created by: ntvu (23/05/2023)
        public Task<IEnumerable<FixedAssetDto>> FilteringAndPagingAsync(LimitPage limit, FilterModel filter);

        /// <summary>
        /// Hàm gọi tới repo, lấy id của tài sản.
        /// </summary>
        /// <param name="fixedAssetCode">mã tài sản</param>
        /// <returns>Guid</returns>
        /// Created by: ntvu (23/05/2023)
        public Task<Guid> GetFixedAssetIdAsync(string fixedAssetCode);

        /// <summary>
        /// Hàm gọi tới repo, check trùng mã
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <param name="fixedAssetCode">Mã tài sản cần check</param>
        /// <returns>int</returns>
        /// created by: ntvu (28/05/2023)
        public Task<int> CheckDuplicateCodeAsync(Guid fixedAssetId, string fixedAssetCode);

        /// <summary>
        /// Hàm gọi tới repo, tạo mã mới
        /// </summary>
        /// <returns>string</returns>
        /// created by: ntvu (28/05/2023)
        public Task<string> GenerateNewCodeAsync();

        /// <summary>
        /// Hàm gọi tới repo, lấy thông số summary
        /// </summary>
        /// <returns>Task<Summary></returns>
        /// created by: ntvu (06/06/2023)
        public Task<Summary> GetSummaryAsync();

        /// <summary>
        /// Hàm gọi tới repo, export file excel
        /// </summary>
        /// <returns>Task<FileStreamResult></returns>
        /// created by: ntvu (19/06/2023)
        public Task<FileContentResult> ExportFixedAssetToExcel();

        /// <summary>
        /// hàm gọi tới repo, import excel thành  object
        /// </summary>
        /// <returns></returns>
        public List<FixedAssetImport> ImportExcel<FixedAssetImport>(ImportModel importModel);
        #endregion
    }
}
