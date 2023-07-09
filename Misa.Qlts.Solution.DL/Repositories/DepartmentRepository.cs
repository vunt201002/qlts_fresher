using Dapper;
using Misa.Qlts.Solution.DL.Base;
using Misa.Qlts.Solution.DL.Context;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;
using System.Data;

namespace Misa.Qlts.Solution.DL.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(DapperContext context) : base(context)
        {
        } 
        #endregion
    }
}
