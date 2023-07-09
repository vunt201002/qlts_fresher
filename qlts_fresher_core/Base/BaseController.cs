using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos;
using Misa.Qlts.Solution.Common.Exceptions;
using Misa.Qlts.Solution.Common.Exceptions.ValidateException;

namespace Misa.Qlts.Solution.Controller.Base
{   
    [ApiController]
    public abstract class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        #region Properties
        protected readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        #endregion

        #region Constructors
        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Phương thức get, gọi tới service lấy bản ghi.
        /// </summary>
        /// <param name="Id">Id bản ghi</param>
        /// <returns>IActionResult</returns>
        /// Created by: ntvu (15/05/2023)
        [HttpGet("{Id}")]
        public virtual async Task<TEntityDto> GetAsync(Guid Id)
        {
            var entityDto = await _baseService.GetAsync(Id);

            return entityDto;
        }

        /// <summary>
        /// Phương thức get, gọi tới service, lấy tất cả bản ghi.
        /// </summary>
        /// <returns>IActionResult</returns>
        /// Created by: ntvu (15/05/2023)
        [HttpGet]
        public async Task<IEnumerable<TEntityDto>> GetListAsync()
        {
            var entities = await _baseService.GetListAsync();

            return entities;
        }

        /// <summary>
        /// Phương thức post, gọi tới service, thêm một bản ghi
        /// </summary>
        /// <param name="tEntityCreateDto">Bản ghi cần thêm</param>
        /// <returns>IActionResult</returns>
        /// Created by: ntvu (15/05/2023)
        [HttpPost("new")]
        public async Task<int> AddAsync([FromBody] TEntityCreateDto tEntityCreateDto)
        {
            int res = await _baseService.AddAsync(tEntityCreateDto);

            return res;
        }

        /// <summary>
        /// Phương thức put gọi tới service, chỉnh sửa bản ghi
        /// </summary>
        /// <param name="Id">Id bản ghi</param>
        /// <param name="tEntityUpdateDto">Thông tin chỉnh sửa</param>
        /// <returns>IActionResult</returns>
        /// Created by: ntvu (15/05/2023)
        [HttpPut("{Id}")]
        public async Task<int> UpdateAsync(
            Guid Id,
            [FromBody] TEntityUpdateDto tEntityUpdateDto
        )
        {
            int res = await _baseService.UpdateAsync(Id, tEntityUpdateDto);

            return res;
        }

        /// <summary>
        /// Phương thức delete gọi tới service, xóa bản ghi
        /// </summary>
        /// <param name="Ids">Danh sách id cần xóa</param>
        /// <returns>IActionResult</returns>
        /// Created by: ntvu (15/05/2023)
        [HttpDelete]
        public async Task<int> DeleteAsync([FromBody] string Ids)
        {
            object IdsObj = new { Ids = Ids };
            int res = await _baseService.DeleteAsync(IdsObj);
            return res;
        } 
        #endregion
    }
}
