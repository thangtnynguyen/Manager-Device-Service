using System.Net.Mail;
using System.Net;
using Manager_Device_Service.Services.Interfaces;
using Manager_Device_Service.Domains.Model.Mail;

namespace Manager_Device_Service.Services
{
    public class MailService:IMailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendMail(SendMailRequest request)
        {
            try
            {
                using (MailMessage mail = new MailMessage(_config["Smtp:Username"], request.ToEmail))
                {
                    mail.Subject = request.Subject;
                    mail.Body = request.Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"])))
                    {
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]);
                        await smtp.SendMailAsync(mail);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gửi tin nhắn thất bại: " + ex.Message);
            }
        }
        public async Task<bool> WorkSendMail(SendMailRequest request)
        {
            try
            {
                using (MailMessage mail = new MailMessage(_config["Smtp:Username"], request.ToEmail))
                {
                    mail.Subject = request.Subject;
                    mail.Body = request.Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"])))
                    {
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]);
                        await smtp.SendMailAsync(mail);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gửi tin nhắn thất bại: " + ex.Message);
            }
        }
    }


}
