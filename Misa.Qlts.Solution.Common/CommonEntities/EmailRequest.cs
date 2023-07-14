using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Qlts.Solution.Common.CommonEntities
{
    /// <summary>
    /// thông tin email gửi
    /// </summary>
    /// created by: ntvu (14/07/2023)
    public class EmailRequest
    {
        public string ToEmail { get; set; }         // email nhận
        public string Subject { get; set; }         // tiêu đề mail
        public string Body { get; set; }               // nội dung mail

        public EmailRequest()
        {
            
        }


        public EmailRequest(string ToEmail, string Subject, string Body)
        {
            this.ToEmail = ToEmail;
            this.Subject = Subject;
            this.Body = Body;
        }
    }
}
