
using AutoMapper;
using Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.AutoMapper
{
    /// <summary>
    /// lớp map tài sản
    /// </summary>
    /// created by: ntvu (15/05/2023)
    public class FixedAssetProfile : Profile
    {
        #region Constructor
        public FixedAssetProfile()
        {
            CreateMap<FixedAsset, FixedAssetDto>();
            CreateMap<FixedAsset, FixedAssetCreateDto>();
            CreateMap<FixedAsset, FixedAssetUpdateDto>();
            CreateMap<FixedAssetCreateDto, FixedAsset>();
            CreateMap<FixedAssetUpdateDto, FixedAsset>();
            CreateMap<FixedAssetDto, FixedAsset>();
        } 
        #endregion
    }
}
