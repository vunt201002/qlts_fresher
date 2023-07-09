using System.ComponentModel.DataAnnotations;
using Misa.Qlts.Solution.Common.Enums;

namespace Misa.Qlts.Solution.BL.DepartmentService.DepartmentDtos
{
    /// <summary>
    /// Lớp nhận phòng ban.
    /// </summary>
    /// Created by: ntvu (19/05/2023)
    public class DepartmentDto
    {
        #region Properties
        [Required]
        public Guid department_id { get; set; }                 // id phòng ban

        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string department_code { get; set; }             // mã phòng ban
        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string department_name { get; set; }             // tên phòng ban 
        #endregion
    }
}
