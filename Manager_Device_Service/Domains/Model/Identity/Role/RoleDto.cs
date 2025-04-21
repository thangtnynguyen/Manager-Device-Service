

using Manager_Device_Service.Domains.Model.Identity.Permission;

namespace Manager_Device_Service.Domains.Model.Identity.Role
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? NormalizedName { get; set; }

        public List<PermissionDto> Permissions { get; set; }

    }
}
