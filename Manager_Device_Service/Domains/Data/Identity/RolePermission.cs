using System.Security;

namespace Manager_Device_Service.Domains.Data.Identity
{
    public class RolePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
