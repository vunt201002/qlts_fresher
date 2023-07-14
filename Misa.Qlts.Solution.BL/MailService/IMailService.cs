using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.Qlts.Solution.Common.CommonEntities;

namespace Misa.Qlts.Solution.BL.MailService
{
    /// <summary>
    /// interface mail service
    /// </summary>
    /// created by: ntvu (14/07/2023)
    public interface IMailService
    {
        /// <summary>
        /// hàm gửi mail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns>Task</returns>
        public void SendEmailAsync(EmailRequest emailRequest);

    }
}
