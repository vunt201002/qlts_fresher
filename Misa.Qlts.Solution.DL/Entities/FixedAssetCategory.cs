using System.ComponentModel.DataAnnotations;
using Misa.Qlts.Solution.Common.Enums;
using Misa.Qlts.Solution.DL.Base;

namespace Misa.Qlts.Solution.DL.Entities
{
    /// <summary>
    /// Lớp loại tài sản.
    /// </summary>
    /// Created by: ntvu (10/05/2023)
    public class FixedAssetCategory : BaseEntity
    {
        #region Properties
        [Required]
        [StringLength((int)ValidationEnum.IdLength)]
        public Guid fixed_asset_category_id { get; set; }                   // id loại tài sản


        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string fixed_asset_category_code { get; set; }               // mã loại tài sản


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string fixed_asset_category_name { get; set; }               // tên loại tài sản 
        #endregion
    }
}
