using Manager_Device_Service.Domains.Model.Mail;
using Manager_Device_Service.Services.Interfaces;

namespace Manager_Device_Service.Services
{
    public class BorrowReminderService: IBorrowReminderService
    {
        private readonly IMailService _mailService;

        public BorrowReminderService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public void SendReturnReminderMail(string toEmail, string deviceName, DateTime dueDate)
        {
            var mail = new SendMailRequest
            {
                ToEmail = toEmail,
                Subject = "Nhắc nhở hoàn trả thiết bị",
                Body = $"Thiết bị \"{deviceName}\" cần được hoàn trả trước 23:59 ngày {dueDate:dd/MM/yyyy}."
            };

            _mailService.SendMail(mail);
        }
    }
}
