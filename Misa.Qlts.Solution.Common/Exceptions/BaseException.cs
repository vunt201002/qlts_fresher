

using System.Text.Json;

namespace Misa.Qlts.Solution.Common.Exceptions
{
    /// <summary>
    /// Lớp lỗi cơ sở
    /// </summary>
    /// created by: ntvu (28/05/2023)
    public class BaseException
    {
        #region Properties
        public int ErrorCode { get; set; }                  // mã lỗi
        public string? UserMessage { get; set; }            // thông báo cho người dùng
        public string? DevMessage { get; set; }             // thông báo cho dev
        public string? TraceId { get; set; }                // mã tra cứu
        public string? MoreInfo { get; set; }               // thông tin thêm 
        #endregion

        #region Methods
        /// <summary>
        /// Override phương thức ToString của class Exception
        /// </summary>
        /// <returns>string</returns>
        /// created by: ntvu (28/05/2023)
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        } 
        #endregion
    }

}
