

using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.Identity.Role
{
    public class GetRoleRequest: PagingRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
