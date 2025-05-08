using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.Device;
using Manager_Device_Service.Domains.Model.DeviceCategory;
using Manager_Device_Service.Domains.Model.DeviceLog;
using Manager_Device_Service.Extension;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/device")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/device/create
        [HttpPost("create")]
        public async Task<ApiResult<DeviceDto>> CreateDevice([FromBody] CreateDeviceRequest request)
        {
            request.BaseRoomId=request.RoomId;
            var result = await _unitOfWork.Devices.CreateDeviceAsync(request);
            var createDeviceLogRequest= new CreateDeviceLogRequest()
            {
                DeviceId = result.Id,
                Description = "Tạo thiết bị mới",
                Action = DeviceAction.Imported,
                UserActionId= User.GetUserId()
            };
            await _unitOfWork.DeviceLogs.CreateDeviceLogAsync(createDeviceLogRequest);
            return ApiResult<DeviceDto>.Success("Tạo thiết bị thành công", result);
        }

        // PUT: api/device/update
        [HttpPut("update")]
        public async Task<ApiResult<DeviceDto>> UpdateDevice([FromBody] UpdateDeviceRequest request)
        {
            var result = await _unitOfWork.Devices.UpdateDeviceAsync(request);
            return ApiResult<DeviceDto>.Success("Cập nhật thiết bị thành công", result);
        }

        // PUT: api/device/update-status
        [HttpPut("update-status")]
        public async Task<ApiResult<DeviceDto>> UpdateStatusDevice([FromBody] UpdateStatusDeviceRequest request)
        {
            var result = await _unitOfWork.Devices.UpdateStatusDeviceAsync(request);
            return ApiResult<DeviceDto>.Success("Cập nhật trạng thái thiết bị thành công", result);
        }

        // GET: api/device/paging?name=...&deviceCategoryId=...&roomId=...&status=...&pageIndex=1&pageSize=10
        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<DeviceDto>>> GetDevicesPaging([FromQuery] GetDeviceRequest request)
        {
            var result = await _unitOfWork.Devices.PagingAsync(
                request.Name,
                request.DeviceCategoryId,
                request.RoomId,
                request.SerialNumber,
                request.Keyword,
                request.Status,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<DeviceDto>>.Success("Lấy danh sách thiết bị thành công", result);
        }

        // GET: api/device/get-by-id?id=1
        [HttpGet("get-by-id")]
        public async Task<ApiResult<DeviceByIdDto>> GetDeviceById([FromQuery] EntityIdentityRequest<int> request)
        {
            var device = await _unitOfWork.Devices.GetByIdDetailAsync(request.Id);
            return ApiResult<DeviceByIdDto>.Success("Lấy thông tin thiết bị thành công", _unitOfWork.Mapper.Map<DeviceByIdDto>(device));
        }
    }
}
