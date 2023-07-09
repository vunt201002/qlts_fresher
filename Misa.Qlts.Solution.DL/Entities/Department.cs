using System.ComponentModel.DataAnnotations;
using Misa.Qlts.Solution.Common.Enums;
using Misa.Qlts.Solution.DL.Base;

namespace Misa.Qlts.Solution.DL.Entities
{
    /// <summary>
    /// Lớp phòng ban.
    /// </summary>
    /// Created by: ntvu (10/05/2023)
    public class Department : BaseEntity
    {
        #region Properties
        [Required]
        [StringLength((int)ValidationEnum.IdLength)]
        public Guid department_id { get; set; }                             // id phòng ban
        [Required]
        [StringLength((int)ValidationEnum.CodeLength)]
        public string department_code { get; set; }                         // mã phòng ban
        [Required]
        [StringLength((int)ValidationEnum.NameLength)]
        public string department_name { get; set; }                         // tên phòng ban 
        #endregion
    }
}
