using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.Common.Enums;

namespace Misa.Qlts.Solution.BL.DepartmentService.DepartmentDtos
{
    /// <summary>
    /// Lớp tạo phòng ban.
    /// </summary>
    /// Created by: ntvu (19/05/2023)
    public class DepartmentCreateDto
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
