using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// Lỗi not found (404)
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class NotFoundException : Exception
    {
        #region Constructors
        public NotFoundException()
        { }

        public NotFoundException(string message) : base(message)
        { } 
        #endregion
    }
}
