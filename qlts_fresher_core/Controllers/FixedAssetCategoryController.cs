using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.FixedAssetCategoryService;
using Misa.Qlts.Solution.BL.FixedAssetCategoryService.FixedAssetCategoryDtos;
using Misa.Qlts.Solution.Controller.Base;

namespace qlts_fresher_core.Controllers
{
    [Route("api/fixedAssetCategories")]
    public class FixedAssetCategoryController : BaseController<FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>
    {
        #region Constructors
        public FixedAssetCategoryController(
            IFixedAssetCategoryService fixedAssetCategoryService
        ) : base(fixedAssetCategoryService)
        {
        } 
        #endregion
    }
}
