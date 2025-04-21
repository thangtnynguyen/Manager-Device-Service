

namespace Manager_Device_Service.Domains.Model.Identity.Permission
{
    public class PermissionDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int? ParentPermissionId { get; set; }

        public string Description { get; set; }

        public List<PermissionDto> Childrens { get; set; }
    }
}
