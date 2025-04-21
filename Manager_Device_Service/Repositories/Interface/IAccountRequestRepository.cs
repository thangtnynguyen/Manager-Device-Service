using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.User;
using Manager_Device_Service.Domains.Model.AccountRequest;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IAccountRequestRepository : IRepositoryBase<AccountRequest, int>
    {
        Task<PagingResult<AccountRequestDto>> GetPagingAsync(string? keyword, AccountRequestStatus? status, string? sortBy, string? orderBy, int pageIndex, int pageSize);
        Task<AccountRequestDto> CreateAccountRequestAsync(CreateAccountRequest request);
        Task<AccountRequestDto> UpdateStatusAsync(UpdateAccountRequestStatus request);
    }
}
