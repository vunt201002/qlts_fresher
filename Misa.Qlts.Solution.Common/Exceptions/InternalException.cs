using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// Lỗi internal server (500)
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class InternalException : Exception
    {
        #region Constructors
        public InternalException()
        { }
        public InternalException(string message) : base(message)
        {

        } 
        #endregion
    }
}
