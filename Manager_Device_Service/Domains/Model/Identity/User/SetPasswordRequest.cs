using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class SetPasswordRequest
    {
        public string Email { get; set; }
        public string? ActivationCode { get; set; }
        public string? Otp { get; set; }
        public string? NewPassword { get; set; }
    }
}
