using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions.ValidateException
{
    /// <summary>
    /// Lỗi tỷ lệ hao mòn
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class DepreciationRateException : Exception
    {
        #region Constructors
        public DepreciationRateException()
        {

        }

        public DepreciationRateException(string message) : base(message)
        {

        } 
        #endregion
    }
}
