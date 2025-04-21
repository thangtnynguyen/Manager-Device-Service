using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Model.DeviceCategory;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Manager_Device_Service.Controllers
{
    [Route("api/device-category")]
    [ApiController]
    public class DeviceCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public DeviceCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/device-category/create
        [HttpPost("create")]
        public async Task<ApiResult<DeviceCategoryDto>> CreateDeviceCategory([FromForm] CreateDeviceCategoryRequest request)
        {
            if (request.ImageFile?.Length > 0)
            {
                request.ImageUrl = await _fileService.UploadFileAsync(request.ImageFile, PathFolderConstant.Banner);
            }
            else
            {
                request.ImageUrl = ImageConstant.Image;
            }
            var result = await _unitOfWork.DeviceCategories.CreateDeviceCategoryAsync(request);
            return ApiResult<DeviceCategoryDto>.Success("Tạo danh mục thiết bị thành công", _unitOfWork.Mapper.Map<DeviceCategoryDto>(result));
        }

        // PUT: api/device-category/update
        [HttpPut("update")]
        public async Task<ApiResult<DeviceCategoryDto>> UpdateDeviceCategory([FromBody] UpdateDeviceCategoryRequest request)
        {
            var entity = await _unitOfWork.DeviceCategories.GetByIdAsync(request.Id);
            if (entity == null)
            {
                return ApiResult<DeviceCategoryDto>.Failure("Không tìm thấy danh mục thiết bị");
            }
            _unitOfWork.Mapper.Map(request, entity);
            await _unitOfWork.DeviceCategories.UpdateAsync(entity);
            return ApiResult<DeviceCategoryDto>.Success("Cập nhật danh mục thiết bị thành công", _unitOfWork.Mapper.Map<DeviceCategoryDto>(entity));
        }

        // GET: api/device-category/paging?Name=...&pageIndex=1&pageSize=10
        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<DeviceCategoryDto>>> GetDeviceCategoriesPaging([FromQuery] GetDeviceCategoryRequest request)
        {
            var result = await _unitOfWork.DeviceCategories.PagingAsync(
                request.Name,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<DeviceCategoryDto>>.Success("Lấy danh sách danh mục thiết bị thành công", result);
        }

        // GET: api/device-category/get-by-id?id=1
        [HttpGet("get-by-id")]
        public async Task<ApiResult<DeviceCategoryDto>> GetDeviceCategoryById([FromQuery] EntityIdentityRequest<int> request)
        {
            var entity = await _unitOfWork.DeviceCategories.GetByIdAsync(request.Id);
            return ApiResult<DeviceCategoryDto>.Success("Lấy thông tin danh mục thiết bị thành công", _unitOfWork.Mapper.Map<DeviceCategoryDto>(entity));
        }
    }
}
