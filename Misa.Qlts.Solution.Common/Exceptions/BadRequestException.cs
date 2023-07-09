using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// lỗi bad request
    /// </summary>
    /// created by: ntvu (18/05/2023)
    public class BadRequestException : Exception
    {
        #region Constructors
        public BadRequestException()
        {

        }

        public BadRequestException(string message) : base(message)
        {

        } 
        #endregion
    }
}
