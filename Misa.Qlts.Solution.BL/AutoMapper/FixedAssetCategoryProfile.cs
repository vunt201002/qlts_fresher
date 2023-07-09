using AutoMapper;
using Misa.Qlts.Solution.BL.FixedAssetCategoryService.FixedAssetCategoryDtos;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.AutoMapper
{
    /// <summary>
    /// lớp map loại tài sản
    /// </summary>
    /// created by: ntvu (15/05/2023)
    public class FixedAssetCategoryProfile : Profile
    {
        #region Constructor
        public FixedAssetCategoryProfile()
        {
            CreateMap<FixedAssetCategory, FixedAssetCategoryDto>();
        } 
        #endregion

    }
}
