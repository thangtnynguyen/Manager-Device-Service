using AutoMapper.Internal;
using Hangfire;
using Manager_Device_Service.Core.Constant.Identity;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.Borrow;
using Manager_Device_Service.Domains.Model.Mail;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Services;
using Manager_Device_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/borrow-request")]
    [ApiController]
    public class BorrowRequestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public BorrowRequestController(IUnitOfWork unitOfWork, IMailService mailService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _userService = userService;
        }

        // POST: api/borrow/create
        [HttpPost("create")]
        public async Task<ApiResult<BorrowRequestDto>> CreateBorrowRequest([FromBody] CreateBorrowRequest model)
        {
            var userAction = await _userService.GetUserInfoAsync();
            model.UserActionId = userAction.Id;
            var userAdmins = await _userService.GetUserByRoleAsync(RoleConstant.RoleAdminId);
            var emailUserAdmins = userAdmins.Select(user => user.Email).Where(email => !string.IsNullOrEmpty(email)).ToList();
            var emailUserAdminString = string.Join(",", emailUserAdmins);

            var borrowDto = await _unitOfWork.BorrowRequests.CreateBorrowRequestAsync(model);
            var userActionName = userAction.Name;

            var mailRequest = new SendMailRequest
            {
                ToEmail = emailUserAdminString,
                Subject = "Yêu cầu mượn thiết bị mới",
                Body = $"Có yêu cầu mượn thiết bị từ : {userActionName}. Vui lòng duyệt yêu cầu."
            };
            Task.Run(() => _mailService.SendMail(mailRequest));
            return ApiResult<BorrowRequestDto>.Success("Tạo yêu cầu mượn thành công", borrowDto);
        }

        // PUT: api/borrow/update-status
        [HttpPut("update-status")]
        public async Task<ApiResult<BorrowRequestDto>> UpdateStatusBorrowRequest([FromBody] UpdateStatusBorrowRequest model)
        {
            var borrowRequest = await _unitOfWork.BorrowRequests.GetByIdAsync(model.Id);
            var device = await _unitOfWork.Devices.GetByIdAsync(borrowRequest.DeviceId);
            if(device.Status == DeviceStatus.Borrowed && model.Status == BorrowRequestStatus.Approved)
            {
                return ApiResult<BorrowRequestDto>.Failure("Thiết bị đã được mượn, không thể duyệt yêu cầu mượn");
            }
            // Lấy yêu cầu mượn cũ
            var borrowDto = await _unitOfWork.BorrowRequests.UpdateStatusBorrowRequestAsync(model);
            var getUserActionRequest= new EntityIdentityRequest<int> { Id = borrowDto.UserActionId };
            var userAction = await _userService.GetById(getUserActionRequest);
            // Nếu yêu cầu được duyệt (Approved), cập nhật trạng thái của thiết bị sang Borrowed
            if (borrowDto.Status == BorrowRequestStatus.Approved)
            {
                // Tìm thiết bị theo borrowDto.DeviceId
                if (device != null)
                {
                    device.Status = DeviceStatus.Borrowed;
                    device.RoomId = borrowDto.RoomId;
                    // Tạo log cho thiết bị với Action = Borrowed
                    var log = new Domains.Data.Relate_Device.DeviceLog
                    {
                        DeviceId = device.Id,
                        Action = DeviceAction.Borrowed,
                        Description = $"Thiết bị được mượn bởi  {userAction.Name} vào {borrowDto.BorrowDate}, hạn trả {borrowDto.DueDate}",
                        UserActionId = borrowDto.UserActionId
                    };
                    await _unitOfWork.DeviceLogs.CreateAsync(log);
                    await _unitOfWork.Devices.UpdateAsync(device);
                }

                // Gửi mail thông báo cho người mượn
                var mailToUser = new SendMailRequest
                {
                    ToEmail = userAction.Email,
                    Subject = "Yêu cầu mượn đã được duyệt",
                    Body = $"Yêu cầu mượn thiết bị {borrowDto.DeviceName}- seri: {borrowDto.DeviceSerialNumber} của bạn đã được duyệt. Vui lòng đến phòng lấy thiết bị theo lịch."
                };

                Task.Run(() => _mailService.SendMail(mailToUser));

                if (borrowDto.DueDate.HasValue)
                {
                    var remindTime = borrowDto.DueDate.Value.Date.AddHours(23).AddMinutes(59);

                    BackgroundJob.Schedule<BorrowReminderService>(
                        service => service.SendReturnReminderMail(
                            userAction.Email, // có thể lấy từ thông tin người mượn
                            borrowDto.DeviceName ?? "Thiết bị",
                            borrowDto.DueDate.Value
                        ),
                        remindTime - DateTime.Now // thời gian trì hoãn job
                    );
                }
            }
            else if (borrowDto.Status == BorrowRequestStatus.Returned)
            {
                // Nếu yêu cầu được cập nhật về Returned, cập nhật lại trạng thái thiết bị
                if (device != null)
                {
                    device.Status = DeviceStatus.Available;
                    device.RoomId = device.BaseRoomId;

                    // Tạo log cho thiết bị với Action = Returned
                    var log = new Domains.Data.Relate_Device.DeviceLog
                    {
                        DeviceId = device.Id,
                        Action = DeviceAction.Returned,
                        Description = $"Thiết bị được trả lại bởi  {userAction.Name}, vào {System.DateTime.Now} hạn trả {borrowDto.DueDate}",
                        UserActionId = borrowDto.UserActionId
                    };
                    await _unitOfWork.DeviceLogs.CreateAsync(log);
                    await _unitOfWork.Devices.UpdateAsync(device);
                }

                // Gửi mail cho quản lý thông báo thiết bị đã được trả
                var userAdmins = await _userService.GetUserByRoleAsync(RoleConstant.RoleAdminId);
                var emailUserAdmins = userAdmins.Select(user => user.Email).Where(email => !string.IsNullOrEmpty(email)).ToList();
                var emailUserAdminString = string.Join(",", emailUserAdmins);
                var mailToManager = new SendMailRequest
                {
                    ToEmail = emailUserAdminString,
                    Subject = "Thiết bị đã được trả lại",
                    Body = $"Thiết bị  {borrowDto.DeviceName}- seri: {borrowDto.DeviceSerialNumber} đã được trả lại bởi  {userAction.Name}."
                };
                Task.Run(() => _mailService.SendMail(mailToManager));
            }
            else if (borrowDto.Status == BorrowRequestStatus.Rejected)
            {
                // Gửi mail cho người mượn thông báo yêu cầu mượn bị từ chối
                var mailToUserJejected = new SendMailRequest
                {
                    ToEmail = userAction.Email,
                    Subject = "Yêu cầu mượn thiết bị bị từ chối",
                    Body = $"Thiết bị  {borrowDto.DeviceName}- seri: {borrowDto.DeviceSerialNumber} đã được mượn trước đó, vui lòng chọn thiết bị khác."
                };
                Task.Run(() => _mailService.SendMail(mailToUserJejected));
            }

            return ApiResult<BorrowRequestDto>.Success("Cập nhật trạng thái yêu cầu mượn thành công", borrowDto);
        }

        // GET: api/borrow/paging?keyword=...&pageIndex=1&pageSize=10
        [HttpGet("paging")]
        [HttpGet]
        public async Task<ApiResult<PagingResult<BorrowRequestDto>>> GetBorrowRequestsPaging([FromQuery] GetBorrowRequest request)
        {
            var result = await _unitOfWork.BorrowRequests.PagingAsync(
                request.Keyword,
                request.Class,
                request.RoomName,
                request.Status,
                request.UserActionId,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize
            );

            return ApiResult<PagingResult<BorrowRequestDto>>.Success("Lấy danh sách yêu cầu mượn thành công", result);
        }


        // GET: api/borrow/get-by-id?id=1
        [HttpGet("get-by-id")]
        public async Task<ApiResult<BorrowRequestDto>> GetBorrowRequestById([FromQuery] EntityIdentityRequest<int> request)
        {
            var entity = await _unitOfWork.BorrowRequests.GetByIdAsync(request.Id);
            var dto = _unitOfWork.Mapper.Map<BorrowRequestDto>(entity);
            return ApiResult<BorrowRequestDto>.Success("Lấy thông tin yêu cầu mượn thành công", dto);
        }
    }


}
