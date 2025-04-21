


using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.Identity.Role
{
    public class GetRoleByUserRequest:PagingRequest
    {
        public int UserId { get; set; }
    }
}
