using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.CommonEntities
{
    /// <summary>
    /// class import tài sản
    /// </summary>
    /// created by: ntvu 20/06/2023
    public class FixedAssetImport
    {

        public string fixed_asset_code { get; set; }
        public string fixed_asset_name { get; set; }
        public string fixed_asset_category_name { get; set; }
        public string department_name { get; set; }
        public int quantity { get; set; }
        public decimal cost { get; set; }
        public decimal accumulated_depreciation { get; set; }
        public decimal residual_value { get; set; }
    }
}
