
using Manager_Device_Service.Domains.Model.Mail;

namespace Manager_Device_Service.Services.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendMail(SendMailRequest request);
        Task<bool> WorkSendMail(SendMailRequest request);

    }
}
