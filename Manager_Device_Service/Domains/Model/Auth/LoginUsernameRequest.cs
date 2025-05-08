namespace Manager_Device_Service.Domains.Model.Auth
{
    public class LoginUsernameRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
