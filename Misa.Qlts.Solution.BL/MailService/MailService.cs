using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Misa.Qlts.Solution.Common.CommonEntities;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Misa.Qlts.Solution.BL.MailService
{
    /// <summary>
    /// lớp mail service
    /// </summary>
    /// created by: ntvu (14/07/2023)
    public class MailService : IMailService
    {
        private readonly EmailSetting _emailSetting;

        public MailService(IOptions<EmailSetting> options)
        {
            _emailSetting = options.Value;
        }

        /// <summary>
        /// hàm gửi mail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns>Task</returns>
        public async void SendEmailAsync(EmailRequest emailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_emailSetting.Email);
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = emailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_emailSetting.Host, _emailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSetting.Email, _emailSetting.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
