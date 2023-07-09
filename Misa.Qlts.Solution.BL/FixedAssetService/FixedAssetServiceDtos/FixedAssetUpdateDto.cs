using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.BL.Attributes;
using Misa.Qlts.Solution.Common.Enums;

namespace Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos
{
    /// <summary>
    /// Lớp chỉnh sửa tài sản.
    /// </summary>
    /// Created by: ntvu (19/05/2023)
    public class FixedAssetUpdateDto
    {
        #region Properties
        [Required]
        public Guid fixed_asset_id { get; set; }                        // id tài sản

        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        [DuplicateCode(nameof(fixed_asset_id))]
        public string fixed_asset_code { get; set; }                    // mã tài sản


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string fixed_asset_name { get; set; }                    // tên tài sản

        [Required]
        public Guid department_id { get; set; }                         // id phòng ban

        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string department_code { get; set; }                     // mã phòng ban


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string department_name { get; set; }                     // tên phòng ban

        [Required]
        public Guid fixed_asset_category_id { get; set; }               // id loại tài sản

        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string fixed_asset_category_code { get; set; }           // mã loại tài sản


        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string fixed_asset_category_name { get; set; }           // tên loại tài sản


        [Required]
        public DateTime purchase_date { get; set; }                     // ngày mua

        [Required]
        public DateTime started_use_date { get; set; } = DateTime.Now;  // ngày bắt đầu sử dụng

        [Required]
        public decimal cost { get; set; }                               // nguyên giá

        [Required]
        public int quantity { get; set; }                               // số lượng

        [Required]
        [DepreciationRate(nameof(life_time))]
        public float depreciation_rate { get; set; }                    // tỷ lệ hao mòn (%)

        [Required]
        [AmortizationOfYear(nameof(cost), nameof(depreciation_rate))]
        public decimal amortization_of_year { get; set; }               // giá trị hao mòn năm

        [Required]
        public int tracked_year { get; set; }                           // năm theo dõi

        [Required]
        public int life_time { get; set; }                              // số năm sử dụng

        [Required]
        public decimal accumulated_depreciation { get; set; }           // khấu hao lũy kế

        [Required]
        public decimal residual_value { get; set; }                     // giá trị còn lại 
        #endregion
    }
}
