using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.Common.Enums;

namespace Misa.Qlts.Solution.BL.FixedAssetCategoryService.FixedAssetCategoryDtos
{
    /// <summary>
    /// Lớp tạo loại tài sản.
    /// </summary>
    /// Created by: ntvu (19/05/2023)
    public class FixedAssetCategoryCreateDto
    {
        #region Properties
        [Required]
        public Guid fixed_asset_category_id { get; set; }               // id loại tài sản

        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string fixed_asset_category_code { get; set; }           // mã loại tài sản


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string fixed_asset_category_name { get; set; }           // tên loại tài sản 
        #endregion
    }
}
