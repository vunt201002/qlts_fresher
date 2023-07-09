
using AutoMapper;
using Misa.Qlts.Solution.BL.DepartmentService.DepartmentDtos;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.AutoMapper
{
    /// <summary>
    /// lớp map phòng ban
    /// </summary>
    /// created by: ntvu (15/05/2023)
    public class DepartmentProfile : Profile
    {
        #region Constructor
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
        } 
        #endregion
    }
}
