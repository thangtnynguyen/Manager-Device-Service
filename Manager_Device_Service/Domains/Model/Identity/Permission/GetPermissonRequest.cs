
namespace Manager_Device_Service.Domains.Model.Identity.Permission
{
    public class GetPermissonRequest
    {
        public Guid? RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
