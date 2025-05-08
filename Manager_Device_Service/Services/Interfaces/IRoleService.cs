
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Identity.Role;

namespace Manager_Device_Service.Services.Interfaces
{
    public interface IRoleService
    {
        Task<PagingResult<RoleDto>> GetPaging(GetRoleRequest request);

        Task<RoleDto> GetById(EntityIdentityRequest<int> request);

        Task<PagingResult<RoleDto>> GetByUser(GetRoleByUserRequest request);

        Task<RoleDto> Create(CreateRoleRequest request);

        Task<RoleDto> Edit(EditRoleRequest request);

        Task<RoleDto> Delete(int id);

        Task<List<RoleDto>> DeleteMultiple(List<int?> ids);

        Task<List<User>> GetUsersInRoleByIdAsync(int roleId);


    }
}
