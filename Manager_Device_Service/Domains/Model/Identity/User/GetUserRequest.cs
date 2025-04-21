


using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class GetUserRequest:PagingRequest
    {

        public string? Name { get; set; }
        //public int? BranchId { get; set; }
        //public string? BranchName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        //public DateTime? CreatedAt  { get; set; }
        public bool? IsDeleted { get; set; }



    }
}
