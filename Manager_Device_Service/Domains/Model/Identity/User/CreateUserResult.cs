using Microsoft.AspNetCore.Identity;

namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class CreateUserResult
    {
        public IdentityResult? AddUserResult { get; set; }

        public IdentityResult? AddRoleResult { get; set; }

    }
}
