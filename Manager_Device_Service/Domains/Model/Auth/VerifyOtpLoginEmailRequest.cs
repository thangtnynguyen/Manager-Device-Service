namespace Manager_Device_Service.Domains.Model.Auth
{
    public class VerifyOtpLoginEmailRequest
    {
        public string? Name { get; set; }

        public string Email { get; set; }

        public string Otp { get; set; }

        public string? Password { get;set; }
    }
}
