using Microsoft.AspNetCore.Identity;

namespace Manager_Device_Service.Domains.Data.Identity
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public JobTitle? JobTitle { get; set; }

        public string? AvatarUrl { get; set; }

        public int? Sex { get; set; }

        public string? Address { get; set; }

        public DateTime? BirthDay { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public string? RefreshToken { get; set; }

        public bool? IsRefreshToken { get; set; }

        public bool IsActivated { get; set; } = false; // Mặc định chưa kích hoạt

        public bool? IsLockAccount { get; set; } = false; // Mặc định chưa bị khóa

        public string Position { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; set; }


    }


    public enum JobTitle
    {
        Teacher,
        Student,
        Staff
    }


}