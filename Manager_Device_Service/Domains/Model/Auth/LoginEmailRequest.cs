using System.ComponentModel.DataAnnotations;

namespace Manager_Device_Service.Domains.Model.Auth
{
    public class LoginEmailRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
