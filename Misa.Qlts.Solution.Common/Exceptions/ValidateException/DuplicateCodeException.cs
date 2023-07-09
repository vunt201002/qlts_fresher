using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions.ValidateException
{
    /// <summary>
    /// lỗi trùng mã
    /// </summary>
    /// created by: ntvu (29/05/2023)
    public class DuplicateCodeException : Exception
    {
        #region Constructors
        public DuplicateCodeException()
        {

        }

        public DuplicateCodeException(string message) : base(message)
        {

        } 
        #endregion
    }
}
