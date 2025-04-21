

using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Model.Identity.Permission;

namespace Manager_Device_Service.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<PagingResult<PermissionDto>> GetPaging(GetPermissionRequest request);

        Task<List<PermissionDto>> GetByRoleId(int roleId);

        Task<PermissionDto> Create(CreatePermissionRequest request);

    }
}
