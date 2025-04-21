﻿

namespace Manager_Device_Service.Domains.Model.Identity.Role
{
    public class EditRoleRequest
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? NormalizedName {  get; set; }

        public List<int>? PermissionIds { get; set; }



    }
}
