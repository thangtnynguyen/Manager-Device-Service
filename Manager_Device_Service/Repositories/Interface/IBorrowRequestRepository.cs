using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains.Model.Borrow;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IBorrowRequestRepository : IRepositoryBase<BorrowRequest, int>
    {
        Task<BorrowRequestDto> CreateBorrowRequestAsync(CreateBorrowRequest model);

        Task<BorrowRequestDto> UpdateStatusBorrowRequestAsync(UpdateStatusBorrowRequest model);

        Task<PagingResult<BorrowRequestDto>> PagingAsync(string? keyword, string? Class, string? roomName, BorrowRequestStatus? status, int? userActionId, string? sortBy, string? orderBy, int pageIndex, int pageSize);
    }
}
