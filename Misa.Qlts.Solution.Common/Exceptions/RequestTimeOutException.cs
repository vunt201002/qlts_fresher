using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// lỗi Request Time-out
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class RequestTimeOutException : Exception
    {
        #region Constructors
        public RequestTimeOutException()
        {

        }

        public RequestTimeOutException(string message) : base(message)
        {

        } 
        #endregion
    }
}
