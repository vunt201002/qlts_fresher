using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// lỗi máy chủ không thể thực hiện yêu cầu
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class NotImplementException : Exception
    {
        #region Constructors
        public NotImplementException()
        {

        }

        public NotImplementException(string message) : base(message)
        {

        } 
        #endregion
    }
}
