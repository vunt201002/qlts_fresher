
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.FixedAssetService;
using Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos;
using Misa.Qlts.Solution.Common.CommonEntites;
using Misa.Qlts.Solution.Controller.Base;
using Misa.Qlts.Solution.Common.Exceptions;
using ClosedXML.Excel;
using Misa.Qlts.Solution.DL.Entities;
using System.Data;
using Misa.Qlts.Solution.Common.CommonEntities;

namespace qlts_fresher_core.Controllers
{
    [Route("api/fixedAssets")]
    public class FixedAssetController : BaseController<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        #region Properties
        private readonly IFixedAssetService _fixedAssetService;
        #endregion

        #region Constructors
        public FixedAssetController(
            IFixedAssetService fixedAssetService
        ) : base(fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Phương thức get gọi tới service, lấy tất cả tài sản
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <returns>IActionResult</returns>
        /// Created by: ntvu (15/05/2023)
        [HttpGet("/filter")]
        public async Task<IActionResult> GetFixedAssets([FromQuery] LimitPage limit)
        {
            var fixedAssetsResRes = await _fixedAssetService.GetFixedAssetsAsync(limit);

            return Ok(fixedAssetsResRes);
        }


        /// <summary>
        /// Phương thức post gọi tới service, lấy ra tài sản theo điều kiện và phân trang
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>IActionResult</returns>
        /// Created by: ntvu (15/05/2023)
        [HttpPost]
        public async Task<IEnumerable<FixedAssetDto>> FilteringAndPaging(
            [FromQuery] LimitPage limit,
            [FromBody] FilterModel filter
        )
        {
            var fixedAssetsRes = await _fixedAssetService.FilteringAndPagingAsync(limit, filter);

            return fixedAssetsRes;
        }

        /// <summary>
        /// Phương thức getgọi tới service, lấy id của tài sản.
        /// </summary>
        /// <param name="fixedAssetCode">mã tài sản</param>
        /// <returns>Guid</returns>
        /// Created by: ntvu (23/05/2023)
        [HttpGet("/code/{fixedAssetCode}")]
        public async Task<Guid> GetFixedAssetId([FromRoute] string fixedAssetCode)
        {
            var fixedAssetId = await _fixedAssetService.GetFixedAssetIdAsync(fixedAssetCode);

            return fixedAssetId;
        }

        /// <summary>
        /// Hàm gọi tới service, check trùng mã
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <param name="fixedAssetCode">Mã tài sản cần check</param>
        /// <returns>int</returns>
        /// created by: ntvu (28/05/2023)
        [HttpPost("/duplicate/{fixedAssetId}/{fixedAssetCode}")]
        public async Task<int> CheckDuplicadeCode(
            [FromRoute] Guid fixedAssetId,
            [FromRoute] string fixedAssetCode
        )
        {
            int res = await _fixedAssetService.CheckDuplicateCodeAsync(
                fixedAssetId,
                fixedAssetCode
            );

            return res;
        }

        /// <summary>
        /// Hàm gọi tới service, tạo mã mới
        /// </summary>
        /// <returns>string</returns>
        /// created by: ntvu (28/05/2023)
        [HttpGet("/newcode")]
        public async Task<string> GenerateNewCode()
        {
            string newCode = await _fixedAssetService.GenerateNewCodeAsync();

            return newCode;
        }

        [HttpGet("/summary")]
        public async Task<IActionResult> GetSummary()
        {
            var ResSummary = await _fixedAssetService.GetSummaryAsync();

            return Ok(ResSummary);
        }

        [HttpGet("/api/export")]
        public async Task<FileContentResult> ExportFixedAssetToExcel()
        {
            var fileRes = await _fixedAssetService.ExportFixedAssetToExcel();

            return fileRes;
        }

        [HttpPost("/api/import")]
        public IActionResult ImportExcel([FromBody] ImportModel importModel)
        {
            var fixedAssetImports = _fixedAssetService.ImportExcel<FixedAssetImport>(importModel);

            if (fixedAssetImports == null)
            {
                return NotFound();
            }

            return Ok(fixedAssetImports);
        }

        #endregion
    }
}
