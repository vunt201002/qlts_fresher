using System;
using System.ComponentModel.DataAnnotations;
using Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos;

namespace Misa.Qlts.Solution.BL.Attributes
{
    /// <summary>
    /// Attribute validate trùng mã
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class DuplicateCodeAttribute : ValidationAttribute
    {
        #region Properties
        private readonly string fixed_asset_id;         // id tài sản 
        #endregion

        #region Constructor
        public DuplicateCodeAttribute(string fixed_asset_id)
        {
            this.fixed_asset_id = fixed_asset_id;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Override hàm IsValid, validate việc trùng mã
        /// </summary>
        /// <param name="value">object chứa giá trị</param>
        /// <param name="validationContext"></param>
        /// <returns>ValidationResult</returns>
        /// created by: ntvu (28/05/2023)
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var entityDto = validationContext.ObjectInstance;

            var idValue = (Guid)validationContext
                            .ObjectType
                            .GetProperty(fixed_asset_id)?
                            .GetValue(entityDto);

            return ValidationResult.Success;
        } 
        #endregion
    }
}
