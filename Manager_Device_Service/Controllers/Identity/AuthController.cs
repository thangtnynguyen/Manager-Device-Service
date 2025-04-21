using AutoMapper;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Auth;
using Manager_Device_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Manager_Device_Service.Controllers.Identity
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IAuthService authService, IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login-by-email")]
        public async Task<ApiResult<LoginResult>> LoginByEmail([FromBody] LoginEmailRequest request)
        {
            //Check exists user
            var user = _userManager.Users.FirstOrDefault(user => user.Email == request.Email && user.EmailConfirmed == true);

            if (user == null)
            {
                return new ApiResult<LoginResult>()
                {
                    Status = false,
                    Message = "Email người dùng không tồn tại!",
                    Data = null
                };
            }
            if (user.IsActivated == false)
            {
                return new ApiResult<LoginResult>()
                {
                    Status = false,
                    Message = "Tài khoản chưa kích hoạt",
                    Data = null
                };
            }
            if (user.IsLockAccount == true)
            {
                return new ApiResult<LoginResult>()
                {
                    Status = false,
                    Message = "Tài khoản đã bị khóa!",
                    Data = null
                };
            }
            //Verify login
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

            if (!result.Succeeded)
            {
                return new ApiResult<LoginResult>()
                {
                    Status = false,
                    Message = "Tài khoản hoặc mật khẩu người dùng không chính xác!",
                    Data = null
                };
            }

            //Create token
            var token = await _authService.CreateToken(user);
            var refreshToken = _authService.CreateRefreshToken();

            var refreshTokenValidityInDays = _config["JwtTokenSettings:RefreshTokenValidityInDays"];

            if (string.IsNullOrEmpty(refreshTokenValidityInDays))
            {
                throw new ArgumentNullException(nameof(refreshTokenValidityInDays), "Không thể tải cấu hình RefreshTokenValidityInDays Jwt!");
            }

            var refreshTokenExpiryTime = DateTime.Now.AddDays(int.Parse(refreshTokenValidityInDays));

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            await _userManager.UpdateAsync(user);


            var loginResult = new LoginResult()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.Now.AddDays(30)
            };


            return new ApiResult<LoginResult>()
            {
                Status = true,
                Message = "Đăng nhập thành công!",
                Data = loginResult
            };


        }



        [HttpPost]
        [Route("register-by-email")]
        public async Task<ApiResult<bool>> RegisterByEmail([FromBody] Domains.Model.Auth.RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = "Email đã được sử dụng!",
                    Data = false
                };
            }

            var newUser = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                EmailConfirmed = true, 
                IsActivated = true,   
                IsLockAccount = false
            };

            var createResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!createResult.Succeeded)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = string.Join("; ", createResult.Errors.Select(e => e.Description)),
                    Data = false
                };
            }

            return new ApiResult<bool>
            {
                Status = true,
                Message = "Đăng ký thành công!",
                Data = true
            };
        }



    }
}
