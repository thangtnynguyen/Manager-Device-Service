using AutoMapper;
using Manager_Device_Service.Domains.Data.User;
using Manager_Device_Service.Domains.Model.AccountRequest;

namespace Manager_Device_Service.Mappers
{

    public class AccountRequestMapper : Profile
    {
        public AccountRequestMapper()
        {
            CreateMap<CreateAccountRequest, AccountRequest>();
            CreateMap<AccountRequest, AccountRequestDto>();
        }
    }
}
