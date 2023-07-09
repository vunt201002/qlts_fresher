using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.BL.FixedAssetCategoryService.FixedAssetCategoryDtos;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.FixedAssetCategoryService
{
    public class FixedAssetCategoryService : BaseService<FixedAssetCategory, FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>, IFixedAssetCategoryService
    {
        #region Constructors
        public FixedAssetCategoryService(
            IFixedAssetCategoryRepository fixedAssetCategoryRepository,
            IMapper mapper
        ) : base(fixedAssetCategoryRepository, mapper)
        {
        } 
        #endregion
    }
}
