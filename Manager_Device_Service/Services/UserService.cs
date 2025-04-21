using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Exception;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Identity.User;
using Manager_Device_Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manager_Device_Service.Services
{
    public class UserService: IUserService
    {
        private readonly ManagerDeviceContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly FileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private MailService _mailService;

        public UserService(ManagerDeviceContext dbContext, UserManager<User> userManager, FileService fileService, IHttpContextAccessor httpContextAccessor, IMapper mapper, RoleManager<Role> roleManager, MailService mailService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        
        public async Task<UserDto> GetById(EntityIdentityRequest<int> request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());

                var userDto = _mapper.Map<UserDto>(user);

                return userDto;

            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        public async Task<UserDto> GetUserInfo(HttpContext httpContext)
        {
            try
            {
                var email = httpContext.User.Claims.First(x => x.Type == "Email").Value;

                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                {
                    throw new ApiException("Không tìm thấy người dùng hợp lệ!", HttpStatusCodeConstant.BadRequest);
                }

                var permissions = await GetPermissionByUserAsync(user);

                //var roles= await GetRoleAsync(user);
                var roles = await GetRoleNormalizedAsync(user);



                var userDto = _mapper.Map<UserDto>(user);


                userDto.Permissions = permissions;

                userDto.RoleNames = roles;




                return userDto;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        public async Task<UserDto?> GetUserInfoAsync()
        {
            try
            {
                var email = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "Email").Value;

                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                {
                    throw new ApiException("Không tìm thấy người dùng hợp lệ!", HttpStatusCodeConstant.BadRequest);
                }

                var permissions = await GetPermissionByUserAsync(user);

                //var roles = await GetRoleAsync(user);
                var roles = await GetRoleNormalizedAsync(user);



                var userDto = _mapper.Map<UserDto>(user);


                userDto.Permissions = permissions;

                userDto.RoleNames = roles;



                return userDto;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        public async Task<User> EditUserInfo(EditUserInfoRequest request)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _dbContext.Users.FindAsync(request.Id);

                    if (user == null)
                    {
                        throw new ApiException("Không tìm thấy user có Id hợp lệ!", HttpStatusCodeConstant.BadRequest);
                    }

                    _mapper.Map(request, user);

                    await _dbContext.SaveChangesAsync();


                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return user;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCodeConstant.InternalServerError, ex);
                }
            }
        }

        public async Task<UserDto> AssignUserToRolesAsync(AssignUserToRoleRequest request)
        {
            if (request.RoleNames == null || !request.RoleNames.Any())
            {
                //throw new ApiException("Danh sách vai trò không hợp lệ!", HttpStatusCodeConstant.BadRequest);
            }

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                throw new ApiException("Không tìm thấy user!", HttpStatusCodeConstant.NotFound);
            }

            try
            {
                var userRoles = _dbContext.UserRoles.Where(ur => ur.UserId == user.Id);
                _dbContext.UserRoles.RemoveRange(userRoles);
                await _dbContext.SaveChangesAsync();


                var addResult = await _userManager.AddToRolesAsync(user, request.RoleNames);
                if (!addResult.Succeeded)
                {
                    var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                    throw new Exception($"Có lỗi xảy ra khi thêm vai trò: {errors}");
                }
                if (!addResult.Succeeded)
                {
                    var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                    throw new Exception($"Có lỗi xảy ra khi thêm vai trò: {errors}");
                }

                user.IsRefreshToken = true;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                    throw new Exception($"Có lỗi xảy ra khi cập nhật user: {errors}");
                }

                var userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            catch (Exception ex)
            {
                throw new ApiException($"Có lỗi xảy ra khi gán vai trò cho user: {ex.Message}", HttpStatusCodeConstant.InternalServerError);
            }
        }
      
        public async Task<List<string>> GetRoleByUserAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }


        private async Task<List<string>> GetRoleNormalizedAsync(User user)
        {
            var userRoles = await _dbContext.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            var normalizedRoles = new List<string>();

            foreach (var roleId in userRoles)
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role != null)
                {
                    normalizedRoles.Add(role.NormalizedName);
                }
            }

            return normalizedRoles;
        }



        public async Task<List<string>> GetPermissionByUserAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = await _dbContext.Roles.Where(role => roles.Contains(role.Name))
            .SelectMany(role => role.RolePermissions)
            .Select(rolePermission => rolePermission.Permission.Name).ToListAsync();

            return permissions;
        }

        private async Task<List<string>> GetUserWithRolePermission(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = await _dbContext.Roles.Where(role => roles.Contains(role.Name))
            .SelectMany(role => role.RolePermissions)
            .Select(rolePermission => rolePermission.Permission.Name).ToListAsync();

            return permissions;
        }


        public async Task<ConfirmEmailResult> VerifyEmailWithOtp(string? email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new ConfirmEmailResult
                {
                    Success = false,
                    ErrorMessage = "Không tồn tại người dùng trong hệ thống"
                };
            }

            var otpValid = await _userManager.VerifyChangePhoneNumberTokenAsync(user, otp, user.PhoneNumber);

            if (!otpValid)
            {

                return new ConfirmEmailResult
                {
                    Success = false,
                    ErrorMessage = "OTP đã hết hạn. Vui lòng yêu cầu mã OTP mới"
                };

            }

            return new ConfirmEmailResult
            {
                Success = true
            };
        }

     

    }
}
