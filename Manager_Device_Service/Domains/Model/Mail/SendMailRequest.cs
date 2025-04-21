
namespace Manager_Device_Service.Domains.Model.Mail
{
    public class SendMailRequest
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

}
