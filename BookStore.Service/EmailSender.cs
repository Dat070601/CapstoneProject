using BookStore.Models.DTOs.Settings;
using BookStore.Service.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BookStore.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings mailSettings;
        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }
        public async Task SendEmailVerificationAsync(string toEmail, string code, string emailFor)
        {
            try
            {
                var api = "https://localhost:7149/api/user/" + emailFor + "?code=" + code;
                string subject = "";
                string body = "";

                if (emailFor == "verify-account")
                {
                    subject = "BookStore - Xác thực Email để kích hoạt tài khoản!";
                    body = "<h3>BƯỚC CUỐI CÙNG ĐẺ KÍCH HOẠT TÀI KHOẢN.</h3> " +
                       "<br/>Vui lòng click vào link để xác nhận Email của tài khoản" +
                       "<a href =" + api + "> Verify Account link</a>";
                }
                else if (emailFor == "reset-password")
                {
                    subject = "BookStore - Thay đổi mật khẩu";
                    body = "XIN CHÀO! , <br/><br/>Chúng tôi nhận được yêu cầu thay đổi mật khẩu của bạn. Vui lòng click vào link bên dưới để thay đổi" +
                        "<br/><br/><a href =" + api + ">Reset Password link</a>";
                }
                else if (emailFor == "reset-password")
                {
                    subject = "BookSotre - Thay đổi mật khẩu";
                    body = "<h1>XIN CHÀO!</h1> , <br/><br/>Chúng tôi nhận được yêu cầu thay đổi mật khẩu của bạn. Vui lòng click vào link bên dưới để thay đổi" +
                        "<br/><br/><a href =" + api + ">Reset Password link</a>";
                }

                var builder = new BodyBuilder
                {
                    HtmlBody = body
                };

                var email = new MimeMessage
                {
                    Body = builder.ToMessageBody(),
                    Sender = MailboxAddress.Parse(mailSettings.Mail)
                };

                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                using var client = new MailKit.Net.Smtp.SmtpClient();
                client.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                client.Authenticate(mailSettings.Mail, mailSettings.Password);
                await client.SendAsync(email);
                client.Disconnect(true);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
