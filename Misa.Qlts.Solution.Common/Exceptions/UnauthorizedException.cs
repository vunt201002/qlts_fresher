using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// lỗi chưa xác thực
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class UnauthorizedException : Exception
    {
        #region Constructors
        public UnauthorizedException()
        {

        }

        public UnauthorizedException(string message) : base(message)
        {

        } 
        #endregion
    }
}
