
using AutoMapper;
using Misa.Qlts.Solution.BL.Base;
using Misa.Qlts.Solution.BL.DepartmentService.DepartmentDtos;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.DepartmentService
{
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Constructors
        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IMapper mapper
        ) : base(departmentRepository, mapper)
        {
        } 
        #endregion
    }
}
