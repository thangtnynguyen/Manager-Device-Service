using System.Text.Json.Serialization;

namespace Manager_Device_Service.Domains.Model.Identity.Role
{
    public class CreateRoleRequest
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public string? NormalizedName {  get; set; }

        public List<int>? PermissionIds { get; set; }



    }
}
