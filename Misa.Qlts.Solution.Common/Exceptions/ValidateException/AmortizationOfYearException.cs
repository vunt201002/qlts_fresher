using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Exceptions.ValidateException
{
    /// <summary>
    /// lỗi hao mòn năm
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class AmortizationOfYearException : Exception
    {
        #region Constructors
        public AmortizationOfYearException()
        {

        }

        public AmortizationOfYearException(string message) : base(message)
        {

        } 
        #endregion
    }
}
