using Manager_Device_Service.Core.Constant.Identity;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Identity.User;

namespace Manager_Device_Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(EntityIdentityRequest<int> request);

        Task<UserDto> GetUserInfo(HttpContext httpContext);

        Task<UserDto> GetUserInfoAsync();

        Task<List<string>> GetRoleByUserAsync(User user);

        Task<List<string>> GetPermissionByUserAsync(User user);

        Task<User> EditUserInfo(EditUserInfoRequest request);

        Task<UserDto> AssignUserToRolesAsync(AssignUserToRoleRequest request);

        Task<ConfirmEmailResult> VerifyEmailWithOtp(string? email, string otp);

        Task<List<UserDto>> GetUserByRoleAsync(int roleId = RoleConstant.RoleAdminId);



    }
}
