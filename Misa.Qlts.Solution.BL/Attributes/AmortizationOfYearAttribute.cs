using System.ComponentModel.DataAnnotations;
using Misa.Qlts.Solution.Common.Resources;

namespace Misa.Qlts.Solution.BL.Attributes
{
    /// <summary>
    /// Attribute validate giá trị hao mòn năm
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class AmortizationOfYearAttribute : ValidationAttribute
    {
        #region Properties
        private readonly string cost;                   // nguyên giá
        private readonly string depreciation_rate;      // tỷ lệ hao mòn (%) 
        #endregion

        #region Constructor
        public AmortizationOfYearAttribute(string cost, string depreciation_rate)
        {
            this.cost = cost;
            this.depreciation_rate = depreciation_rate;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Override hàm ValidationResult, thực hiện validate giá trị
        /// hao mòn năm
        /// </summary>
        /// <param name="value">object chứa giá trị hao mòn năm</param>
        /// <param name="validationContext"></param>
        /// <returns>ValidationResult</returns>
        /// created by: ntvu (28/05/2023)
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            // lấy ra đối tượng validate
            var entityDto = validationContext.ObjectInstance;

            // lấy ra nguyên giá
            var costValue = (decimal)validationContext
                            .ObjectType
                            .GetProperty(cost)?
                            .GetValue(entityDto);

            // lấy ra tỷ lệ hao mòn
            var depreciationRate = (float)validationContext
                            .ObjectType
                            .GetProperty(depreciation_rate)?
                            .GetValue(entityDto);

            // Ép kiểu các giá trị
            var floatValue = Convert.ToDecimal(value);
            var expectedValue = (decimal)(depreciationRate / 100) * (decimal)costValue;

            // Validate khi các giá trị khác null
            if (value != null && costValue != null && depreciationRate != null)
            {
                // nếu hao mòn năm khác nguyên giá nhân số năm sử dụng, trả về lỗi
                if (decimal.Compare(floatValue, expectedValue) != 0)
                {
                    // lấy thông báo lỗi từ resource
                    string errorMessage = ValidateErrorMessage
                        .AmortizationOfYearEqualCostMultiRate;

                    return new ValidationResult(errorMessage);
                }

                // nếu hao mòn năm lớn hơn nguyên giá, trả về lỗi
                else if (decimal.Compare((decimal)costValue, floatValue) == -1)
                {
                    // lấy thông báo lỗi từ resource
                    string errorMessage = ValidateErrorMessage
                        .AmortizationOfYearSmaillThanCost;

                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        }
        #endregion
    }
}
