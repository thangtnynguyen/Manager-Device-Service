using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Model.AccountRequest;
using Manager_Device_Service.Domains.Model.DeviceLog;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/device-log")]
    [ApiController]
    public class DeviceLogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceLogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/device-log/create
        [HttpPost("create")]
        public async Task<ApiResult<DeviceLogDto>> CreateDeviceLog([FromBody] CreateDeviceLogRequest request)
        {
            var result = await _unitOfWork.DeviceLogs.CreateDeviceLogAsync(request);
            return ApiResult<DeviceLogDto>.Success("Tạo lịch sử thiết bị thành công", _unitOfWork.Mapper.Map<DeviceLogDto>(result));
        }

        // GET: api/device-log/paging?deviceId=1&userActionId=...&action=Borrowed&pageIndex=1&pageSize=10
        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<DeviceLogDto>>> GetDeviceLogsPaging([FromQuery] Manager_Device_Service.Domains.Model.DeviceLog.GetDeviceLogRequest request)
        {
            var result = await _unitOfWork.DeviceLogs.PagingAsync(
                request.DeviceId,
                request.UserActionId,
                request.Action,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<DeviceLogDto>>.Success("Lấy danh sách lịch sử thiết bị thành công", result);
        }

        // GET: api/device-log/get-by-id?id=1
        [HttpGet("get-by-id")]
        public async Task<ApiResult<DeviceLogDto>> GetDeviceLogById([FromQuery] EntityIdentityRequest<int> request)
        {
            var entity = await _unitOfWork.DeviceLogs.GetByIdAsync(request.Id);
            return ApiResult<DeviceLogDto>.Success("Lấy thông tin lịch sử thiết bị thành công", _unitOfWork.Mapper.Map<DeviceLogDto>(entity));
        }

        [HttpGet("get-by-device-id")]
        public async Task<ApiResult<PagingResult<DeviceLogDto>>> GetDeviceLogByDeviceId([FromQuery] Manager_Device_Service.Domains.Model.DeviceLog.GetDeviceLogRequest request)
        {
            var result = await _unitOfWork.DeviceLogs.PagingAsync(
                request.DeviceId,
                request.UserActionId,
                request.Action,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<DeviceLogDto>>.Success("Lấy danh sách lịch sử thiết bị theo thiết bị thành công", result);
        }

        [HttpGet("get-by-user-id")]
        public async Task<ApiResult<PagingResult<DeviceLogDto>>> GetDeviceLogByUserId([FromQuery] Manager_Device_Service.Domains.Model.DeviceLog.GetDeviceLogByUserRequest request)
        {
            var result = await _unitOfWork.DeviceLogs.GetByUserPagingAsync(
                request.DeviceId,
                request.Action,
                request.SortBy,
                request.OrderBy,
                request.UserActionId,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<DeviceLogDto>>.Success("Lấy danh sách lịch sử thiết bị theo người dùng thành công", result);
        }

       
    }
}
