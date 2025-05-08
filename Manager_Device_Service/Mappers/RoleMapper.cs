
using AutoMapper;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Identity.Role;

namespace Manager_Device_Service.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {

            CreateMap<Role, CreateRoleRequest>();
            CreateMap<CreateRoleRequest, Role>();
            CreateMap<EditRoleRequest, Role>();
            //CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission)));
            CreateMap<RoleDto, Role>();
        }
    }
}
