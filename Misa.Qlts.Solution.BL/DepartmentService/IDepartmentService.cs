using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.BL.DepartmentService.DepartmentDtos;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.DepartmentService
{
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}
