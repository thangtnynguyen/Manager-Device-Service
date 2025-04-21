namespace Manager_Device_Service.Domains.Model.Identity.Permission
{
    public class CreatePermissionRequest
    {

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int? ParentPermissionId { get; set; }

        public string Description { get; set; }

    }
}
