using AutoMapper;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Identity.User;

namespace Manager_Device_Service.Mappers
{
    public class UserMapper  : Profile
    {
        public UserMapper()
        {

            CreateMap<CreateUserRequest, User>();
            CreateMap<EditUserInfoRequest, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
       

        }
    }
}
