using Manager_Device_Service.Domains.Model.Identity.Role;
using Microsoft.AspNetCore.Identity;


namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class UserDto : IdentityUser<int>
    {
        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? AvatarUrl { get; set; }

        public List<string> Permissions { get; set; }

        public List<RoleDto> Roles { get; set; }

        public List<string> RoleNames { get; set; }

        public string? Address { get; set; }

        public bool? IsRefreshToken { get; set; }

        public string Position { get; set; } = string.Empty;

        public string? Code { get; set; }


        //public int? CityId { get; set; }

        //public string? CityName { get; set; }

        //public int? DistrictId { get; set; }

        //public string? DistrictName { get; set; }

        //public int? WardId { get; set; }

        //public string? WardName { get; set; }





    }
}
