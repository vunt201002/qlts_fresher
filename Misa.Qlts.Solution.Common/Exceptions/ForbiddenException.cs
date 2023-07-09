using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// lỗi forbidden
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class ForbiddenException : Exception
    {
        #region Constructors
        public ForbiddenException()
        {

        }

        public ForbiddenException(string message) : base(message)
        {

        } 
        #endregion
    }
}
