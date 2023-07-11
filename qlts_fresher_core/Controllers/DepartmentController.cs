using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.BL.DepartmentService;
using Misa.Qlts.Solution.BL.DepartmentService.DepartmentDtos;
using Misa.Qlts.Solution.Controller.Base;

namespace qlts_fresher_core.Controllers
{
    [Route("api/departments")]
    public class DepartmentController : BaseController<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        #region Constructors
        public DepartmentController(
            IDepartmentService departmentService
        ) : base(departmentService)
        {
        } 
        #endregion
    }
}
