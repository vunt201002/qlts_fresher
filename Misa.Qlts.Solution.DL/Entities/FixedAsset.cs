using Misa.Qlts.Solution.Common.Enums;
using Misa.Qlts.Solution.DL.Base;
using System.ComponentModel.DataAnnotations;

namespace Misa.Qlts.Solution.DL.Entities
{
    /// <summary>
    /// Lớp tài sản.
    /// </summary>
    /// Created by: ntvu (10/05/2023)
    public class FixedAsset : BaseEntity
    {
        #region Properties
        [Required]
        public Guid fixed_asset_id { get; set; }                            // id tài sản


        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string fixed_asset_code { get; set; }                        // mã tài sản


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string fixed_asset_name { get; set; }                        // tên tài sản


        [Required]
        public Guid department_id { get; set; }                             // id phòng ban

        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string department_code { get; set; }                         // mã phòng ban


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string department_name { get; set; }                         // tên phòng ban


        [Required]
        public Guid fixed_asset_category_id { get; set; }                   // id loại tài sản

        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string fixed_asset_category_code { get; set; }               // mã loại tài sản


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string fixed_asset_category_name { get; set; }               // tên loại tài sản


        [Required]
        public DateTime purchase_date { get; set; } = DateTime.Now;         // ngày mua

        [Required]
        public DateTime started_use_date { get; set; } = DateTime.Now;      // ngày bắt đầu sử dụng

        [Required]
        public decimal cost { get; set; }                                   // nguyên giá

        [Required]
        public int quantity { get; set; }                                   // số lượng

        [Required]
        public float depreciation_rate { get; set; }                        // tỷ lệ hao mòn (%)

        [Required]
        public decimal amortization_of_year { get; set; }                   // giá trị hao mòn năm

        [Required]
        public int tracked_year { get; set; }                               // năm theo dõi

        [Required]
        public int life_time { get; set; }                                  // số năm sử dụng

        [Required]
        public int production_year { get; set; }                            // năm sử dụng

        [Required]
        public decimal accumulated_depreciation { get; set; }               // khấu hao lũy kế

        [Required]
        public decimal residual_value { get; set; }                         // giá trị còn lại 
        #endregion
    }
}
