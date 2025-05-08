using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Data.User;
using Manager_Device_Service.Domains.Model.AccountRequest;
using Manager_Device_Service.Domains.Model.Mail;
using Manager_Device_Service.Repositories.Interface;
using Manager_Device_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/account-request")]
    [ApiController]
    public class AccountRequestController : ControllerBase
    {
        private readonly IAccountRequestRepository _accountRequestRepository;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        public AccountRequestController(IAccountRequestRepository accountRequestRepository, IMailService mailService, UserManager<User> userManager)
        {
            _accountRequestRepository = accountRequestRepository;
            _mailService = mailService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<ApiResult<bool>> Create([FromBody] CreateAccountRequest request)
        {
            var result = await _accountRequestRepository.CreateAccountRequestAsync(request);
            return new ApiResult<bool>
            {
                Status = true,
                Message = result != null ? "Tạo yêu cầu cấp tài khoản thành công!" : "Tạo yêu cầu cấp tài khoản thất bại!",
                Data = result != null
            };
        }

        [HttpPost("update-status")]
        public async Task<ApiResult<bool>> UpdateStatus([FromBody] UpdateAccountRequestStatus request)
        {
            var result = await _accountRequestRepository.UpdateStatusAsync(request);

            if (result.Status == AccountRequestStatus.Approved)
            {
                // 1. Tạo tài khoản mới bằng Identity
                var user = new User
                {   
                    UserName = result.Code,
                    Email = result.Email,
                    Name = result.Name,
                    Code=result.Code,
                    Position = result.Position,
                    PhoneNumber = result.PhoneNumber,
                    EmailConfirmed = true,
                    IsActivated = true,
                };

                var defaultPassword = $"{result.Code}@123";

                var createUserResult = await _userManager.CreateAsync(user, defaultPassword);
                if (!createUserResult.Succeeded)
                {
                   return new ApiResult<bool>
                   {
                       Status = false,
                       Message = "Tạo tài khoản thất bại!",
                       Data = false
                   };
                }

                // 2. Gửi mail thông báo tài khoản đã được cấp
                Task.Run(() => _mailService.SendMail(new SendMailRequest
                {
                    ToEmail = result.Email,
                    Subject = "Tài khoản UTEHY Manange Device đã được cấp",
                    Body = $"Xin chào {result.Name},<br/><br/>" +
                           $"Tài khoản của bạn đã được cấp.<br/>" +
                           $"Tên đăng nhập: <b>{result.Code}</b><br/>" +
                           $"Mật khẩu: <b>{defaultPassword}</b><br/><br/>" +
                           $"Vui lòng đăng nhập và đổi mật khẩu ngay sau khi sử dụng lần đầu.<br/>" +
                           $"Trân trọng."
                }));
                return new ApiResult<bool>
                {
                    Status = true,
                    Message = "Cập nhật trạng thái yêu cầu thành công!",
                    Data = true
                };
            }
            else if (result.Status == AccountRequestStatus.Rejected)
            {
                Task.Run(() => _mailService.SendMail(new SendMailRequest
                {
                    ToEmail = result.Email,
                    Subject = "Yêu cầu cấp tài khoản bị từ chối",
                    Body = $"Xin chào {result.Name},<br/><br/>" +
                           $"Rất tiếc! Yêu cầu cấp tài khoản UTEHY Manange Device của bạn đã bị từ chối.<br/><br/>" +
                           $"Trân trọng."
                }));
                return new ApiResult<bool>
                {
                    Status = true,
                    Message = "Cập nhật trạng thái yêu cầu thành công!",
                    Data = true
                };
            }
            else
            {
                return new ApiResult<bool>
                {
                    Status = true,
                    Message = "Cập nhật trạng thái yêu cầu thành công!",
                    Data = true
                };
            }

        }


        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<AccountRequestDto>>> GetPaging([FromQuery] GetAccountRequestRequest request)
        {
            var result = await _accountRequestRepository.GetPagingAsync(
                request.Keyword,
                request.Status,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize
            );
            return new ApiResult<PagingResult<AccountRequestDto>>
            {
                Status = result != null,
                Message = result != null ? "Lấy danh sách yêu cầu cấp tài khoản thành công!" : "Lấy danh sách yêu cầu cấp tài khoản thất bại!",
                Data = result
            };
        }
    }
}
