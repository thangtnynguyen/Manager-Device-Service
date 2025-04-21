using Manager_Device_Service.Core.Data;
using static System.Collections.Specialized.BitVector32;

namespace Manager_Device_Service.Domains.Data.Identity
{
    public class Permission : EntityBase<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int? ParentPermissionId { get; set; }

        public string? Description { get; set; }

        public Permission? ParentPermission { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }
    }

}
