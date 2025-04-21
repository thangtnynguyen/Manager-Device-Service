using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Model.Identity.Permission;
using Manager_Device_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers.Identity
{
    [Route("api/permission")]
    [ApiController]
    //[HasPermission(PermissionConstant.ManagePermission)]

    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService PermissionService)
        {
            _permissionService = PermissionService;
        }

        [HttpGet("paging")]
        //[HasPermission(PermissionConstant.ManagePermissionView)]
        public async Task<ApiResult<PagingResult<PermissionDto>>> Get([FromQuery] GetPermissionRequest request)
        {
            var result = await _permissionService.GetPaging(request);

            return new ApiResult<PagingResult<PermissionDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh quyền thành công thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-role")]
        //[HasPermission(PermissionConstant.ManagePermissionView)]
        public async Task<ApiResult<List<PermissionDto>>> GetByRoldId([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _permissionService.GetByRoleId(request.Id);

            return new ApiResult<List<PermissionDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh quyền thành công thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        //[HasPermission(PermissionConstant.ManagePermissionCreate)]
        public async Task<PermissionDto> Create([FromBody] CreatePermissionRequest request)
        {

            var permissionDto = await _permissionService.Create(request);

            return permissionDto;

        }
    }
}
