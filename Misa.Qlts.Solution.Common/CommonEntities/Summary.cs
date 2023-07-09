using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.CommonEntities
{
    /// <summary>
    /// Các giá trị summary
    /// </summary>
    /// createed by: ntvu (06/06/2023)
    public class Summary
    {
        #region Properties
        public int total_records { get; set; }                               // tổng số bản ghi

        public decimal total_cost { get; set; }                                   // nguyên giá

        public int total_quantity { get; set; }                                   // số lượng

        public decimal total_accumulated_depreciation { get; set; }               // khấu hao lũy kế

        public decimal total_residual_value { get; set; }                         // giá trị còn lại 
        #endregion
    }
}
