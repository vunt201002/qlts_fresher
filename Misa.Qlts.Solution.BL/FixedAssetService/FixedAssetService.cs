
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos;
using Misa.Qlts.Solution.Common.CommonEntites;
using Misa.Qlts.Solution.Common.CommonEntities;
using Misa.Qlts.Solution.Common.Exceptions;
using Misa.Qlts.Solution.Common.Exceptions.ValidateException;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.FixedAssetService
{
    public class FixedAssetService : BaseService<FixedAsset, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>, IFixedAssetService
    {
        #region Properties
        private readonly IFixedAssetRepository _fixedAssetRepository;
        #endregion

        #region Constructors
        public FixedAssetService(
            IFixedAssetRepository fixedAssetRepository,
            IMapper mapper
        ) : base(fixedAssetRepository, mapper)
        {
            _fixedAssetRepository = fixedAssetRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm gọi tới repo, lấy tất cả tài sản
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <returns>IEnumerable<FixedAssetDto></returns>
        /// Created by: ntvu (19/05/2023)
        public async Task<IEnumerable<FixedAssetDto>> FilteringAndPagingAsync(LimitPage limit, FilterModel filter)
        {
            var fixedAssets = await _fixedAssetRepository.FilteringAndPagingAsync(limit, filter);

            if (fixedAssets == null)
            {
                throw new NotFoundException();
            }

            var fixedAssetRes = _mapper.Map<IEnumerable<FixedAssetDto>>(fixedAssets);

            return fixedAssetRes;
        }

        /// <summary>
        /// Hàm gọi tới repo, lọc và phân trang tài sản
        /// </summary>
        /// <param name="limit">số trang và số bản ghi mỗi trang</param>
        /// <param name="filter">điều kiện lọc</param>
        /// <returns>IEnumerable<FixedAssetDto></returns>
        /// Created by: ntvu (23/05/2023)
        public async Task<IEnumerable<FixedAssetDto>> GetFixedAssetsAsync(LimitPage limit)
        {
            var fixedAssets = await _fixedAssetRepository.GetFixedAssetsAsync(limit);

            if (fixedAssets == null)
            {
                throw new NotFoundException();
            }

            var fixedAssetRes = _mapper.Map<IEnumerable<FixedAssetDto>>(fixedAssets);

            return fixedAssetRes;
        }

        /// <summary>
        /// Hàm gọi tới repo, lấy id của tài sản.
        /// </summary>
        /// <param name="fixedAssetCode">mã tài sản</param>
        /// <returns>Guid</returns>
        /// Created by: ntvu (23/05/2023)
        public async Task<Guid> GetFixedAssetIdAsync(string fixedAssetCode)
        {
            var fixedAssetId = await _fixedAssetRepository.GetFixedAssetIdAsync(fixedAssetCode);

            if (fixedAssetId == default)
            {
                throw new NotFoundException();
            }

            return fixedAssetId;
        }

        /// <summary>
        /// Hàm gọi tới repo, check trùng mã
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <param name="fixedAssetCode">Mã tài sản cần check</param>
        /// <returns>int</returns>
        /// created by: ntvu (28/05/2023)
        public async Task<int> CheckDuplicateCodeAsync(Guid fixedAssetId, string fixedAssetCode)
        {
            var res = await _fixedAssetRepository.CheckDuplicateCodeAsync(
                fixedAssetId,
                fixedAssetCode
            );

            //if (res == 0)
            //{
            //    throw new DuplicateCodeException();
            //}

            return res;
        }

        /// <summary>
        /// Hàm gọi tới repo, tạo mã mới
        /// </summary>
        /// <returns>string</returns>
        /// created by: ntvu (28/05/2023)
        public async Task<string> GenerateNewCodeAsync()
        {
            var newCode = await _fixedAssetRepository.GenerateNewCodeAsync();

            if (newCode == default)
            {
                throw new NotFoundException();
            }

            return newCode;
        }

        /// <summary>
        /// Hàm gọi tới repo, lấy thông số summary
        /// </summary>
        /// <returns>Task<Summary></returns>
        /// created by: ntvu (06/06/2023)
        public async Task<Summary> GetSummaryAsync()
        {
            var ResSummary = await _fixedAssetRepository.GetSummary();

            if (ResSummary == null)
            {
                throw new NotFoundException();
            }

            return ResSummary;
        }

        /// <summary>
        /// Hàm gọi tới repo, export file excel
        /// </summary>
        /// <returns>Task<FileStreamResult></returns>
        /// created by: ntvu (19/06/2023)
        public async Task<FileContentResult> ExportFixedAssetToExcel()
        {
            var fileRes = await _fixedAssetRepository.ExportFixedAssetToExcel();

            return fileRes;
        }

        /// <summary>
        /// hàm gọi tới repo, import excel thành  object
        /// </summary>
        /// <returns></returns>
        public List<FixedAssetImport> ImportExcel<FixedAssetImport>(ImportModel importModel)
        {
            var fixedAssetImmports = _fixedAssetRepository.ImportExcel<FixedAssetImport>(importModel);

            return fixedAssetImmports;
        }
        #endregion
    }
}
