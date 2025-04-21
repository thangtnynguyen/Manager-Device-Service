using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class CreateUserRequest
    {
        public int? EmployeeId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? UrlClient {  get; set; }
    }
}
