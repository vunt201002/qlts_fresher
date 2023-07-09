using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.Enums
{
    /// <summary>
    /// mã lỗi nghiệp vụ
    /// </summary>
    /// created by: ntvu (19/05/2023)
    public enum ErrorCode
    {
        DepreciationRateExceptionCode = 1000,           // mã lỗi tỷ lệ hao mòn
        AmortizationOfYearExceptionCode = 2000,         // mã lỗi hao mòn năm
        DuplicateCodeExceptionCode = 3000               // mã lỗi trùng mã tài sản
    }
}
