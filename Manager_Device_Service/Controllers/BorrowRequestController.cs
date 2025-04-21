using AutoMapper.Internal;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.Borrow;
using Manager_Device_Service.Domains.Model.Mail;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/borrow")]
    [ApiController]
    public class BorrowRequestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        public BorrowRequestController(IUnitOfWork unitOfWork, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        // POST: api/borrow/create
        [HttpPost("create")]
        public async Task<ApiResult<BorrowDto>> CreateBorrowRequest([FromBody] CreateBorrowRequest model)
        {
            var borrowDto = await _unitOfWork.BorrowRequests.CreateBorrowRequestAsync(model);
            var userActionName = "Nguyễn Văn A";
            // Gửi mail thông báo đến quản lý (a@gmail.com và b@gmail.com)
            var mailRequest = new SendMailRequest
            {
                ToEmail = "a@gmail.com, b@gmail.com",
                Subject = "Yêu cầu mượn thiết bị mới",
                Body = $"Có yêu cầu mượn thiết bị từ : {userActionName}. Vui lòng duyệt yêu cầu."
            };
            await Task.Run(() => _mailService.SendMail(mailRequest));
            return ApiResult<BorrowDto>.Success("Tạo yêu cầu mượn thành công", borrowDto);
        }

        // PUT: api/borrow/update-status
        [HttpPut("update-status")]
        public async Task<ApiResult<BorrowDto>> UpdateStatusBorrowRequest([FromBody] UpdateStatusBorrowRequest model)
        {
            // Lấy yêu cầu mượn cũ
            var borrowDto = await _unitOfWork.BorrowRequests.UpdateStatusBorrowRequestAsync(model);
            var userActionName = "Nguyễn Văn A";

            // Nếu yêu cầu được duyệt (Approved), cập nhật trạng thái của thiết bị sang Borrowed
            if (borrowDto.Status == BorrowRequestStatus.Approved)
            {
                // Tìm thiết bị theo borrowDto.DeviceId
                var device = await _unitOfWork.Devices.GetByIdAsync(borrowDto.DeviceId);
                if (device != null)
                {
                    device.Status = DeviceStatus.Borrowed;

                    // Tạo log cho thiết bị với Action = Borrowed
                    var log = new Domains.Data.Relate_Device.DeviceLog
                    {
                        DeviceId = device.Id,
                        Action = DeviceAction.Borrowed,
                        Description = $"Thiết bị được mượn bởi  {userActionName} tại {System.DateTime.Now}",
                        UserActionId = borrowDto.UserActionId
                    };
                    await _unitOfWork.DeviceLogs.CreateAsync(log);
                    await _unitOfWork.Devices.UpdateAsync(device);
                }

                // Gửi mail thông báo cho người mượn
                var mailToUser = new SendMailRequest
                {
                    ToEmail = "c@gmail.com",
                    Subject = "Yêu cầu mượn đã được duyệt",
                    Body = "Yêu cầu mượn của bạn đã được duyệt. Vui lòng đến phòng lấy thiết bị theo lịch."
                };

                await Task.Run(() => _mailService.SendMail(mailToUser));
            }
            else if (borrowDto.Status == BorrowRequestStatus.Returned)
            {
                // Nếu yêu cầu được cập nhật về Returned, cập nhật lại trạng thái thiết bị
                var device = await _unitOfWork.Devices.GetByIdAsync(borrowDto.DeviceId);
                if (device != null)
                {
                    device.Status = DeviceStatus.Available;

                    // Tạo log cho thiết bị với Action = Returned
                    var log = new Domains.Data.Relate_Device.DeviceLog
                    {
                        DeviceId = device.Id,
                        Action = DeviceAction.Returned,
                        Description = $"Thiết bị được trả lại bởi  {userActionName} tại {System.DateTime.Now}",
                        UserActionId = borrowDto.UserActionId
                    };
                    await _unitOfWork.DeviceLogs.CreateAsync(log);
                    await _unitOfWork.Devices.UpdateAsync(device);
                }

                // Gửi mail cho quản lý thông báo thiết bị đã được trả
                var mailToManager = new SendMailRequest
                {
                    ToEmail = "a@gmail.com, b@gmail.com",
                    Subject = "Thiết bị đã được trả lại",
                    Body = $"Thiết bị có ID {borrowDto.DeviceId} đã được trả lại bởi User {borrowDto.UserActionId}."
                };
                await Task.Run(() => _mailService.SendMail(mailToManager));
            }

            return ApiResult<BorrowDto>.Success("Cập nhật trạng thái yêu cầu mượn thành công", borrowDto);
        }

        // GET: api/borrow/paging?keyword=...&pageIndex=1&pageSize=10
        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<BorrowDto>>> GetBorrowRequestsPaging([FromQuery] string? keyword, [FromQuery] string? sortBy, [FromQuery] string? orderBy, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _unitOfWork.BorrowRequests.PagingAsync(keyword, sortBy, orderBy, pageIndex, pageSize);
            return ApiResult<PagingResult<BorrowDto>>.Success("Lấy danh sách yêu cầu mượn thành công", result);
        }

        // GET: api/borrow/get-by-id?id=1
        [HttpGet("get-by-id")]
        public async Task<ApiResult<BorrowDto>> GetBorrowRequestById([FromQuery] EntityIdentityRequest<int> request)
        {
            var entity = await _unitOfWork.BorrowRequests.GetByIdAsync(request.Id);
            var dto = _unitOfWork.Mapper.Map<BorrowDto>(entity);
            return ApiResult<BorrowDto>.Success("Lấy thông tin yêu cầu mượn thành công", dto);
        }
    }


}
