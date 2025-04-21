

using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.Identity.Permission
{
    public class GetPermissionRequest:PagingRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }

    }
}
