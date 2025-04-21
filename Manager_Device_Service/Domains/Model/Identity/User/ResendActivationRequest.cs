using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class ResendActivationRequest
    {
        [Required]
        public List<string> Emails { get; set; } = new List<string>();
        public string? UrlClient { get; set; }
    }
}
