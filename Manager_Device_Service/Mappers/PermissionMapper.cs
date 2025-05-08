using AutoMapper;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Identity.Permission;

namespace Manager_Device_Service.Mappers
{
    public class PermissionMapper : Profile
    {
        public PermissionMapper()
        {

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();
            CreateMap<CreatePermissionRequest, Permission>();

        }
    }
}
