using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.Common.Resources;

namespace Misa.Qlts.Solution.BL.Attributes
{
    /// <summary>
    /// Attribute validate tỷ lệ hao mòn
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class DepreciationRateAttribute : ValidationAttribute
    {
        #region Properties
        private readonly string life_time;          // số năm sử dụng 
        #endregion

        #region Constructor
        public DepreciationRateAttribute(string life_time)
        {
            this.life_time = life_time;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Override hàm IsValid, validate tỷ lệ hao mòn
        /// </summary>
        /// <param name="value">object chứa giá trị</param>
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

            // lấy ra số năm sử dụng
            var lifeTime = (int)validationContext
                            .ObjectType
                            .GetProperty(life_time)?
                            .GetValue(entityDto);

            // thực hiện validate khi các giá trị khác null
            if (value != null && lifeTime != null)
            {
                // Ép kiểu
                var floatValue = Convert.ToSingle(value);
                var ratio = 1f / Convert.ToInt32(lifeTime);

                // nếu tỷ lệ hao mòn khác 1 chia số năm sử dụng,
                // trả về lỗi
                if (Math.Abs(floatValue - ratio) > float.Epsilon)
                {
                    // lấy lỗi từ resource
                    string errorMessage = ValidateErrorMessage
                        .DepreciationRateEqualLifeTimeDividedByOne;

                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        } 
        #endregion
    }
}
