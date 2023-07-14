using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.CommonEntities
{
    /// <summary>
    /// lớp otp
    /// </summary>
    /// created by: ntvu (13/07/2023)
    public class OTP
    {
        public string? otp { get; set; }                                    // otp
        public DateTime otp_expiry_time { get; set; } = DateTime.Now;       // thời gian hết hạn

        public OTP()
        {
            
        }

        public OTP(string otp, DateTime otp_expiry_time)
        {
            this.otp = otp;
            this.otp_expiry_time = otp_expiry_time;
        }
    }
}
