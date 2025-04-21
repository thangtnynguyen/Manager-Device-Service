using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains.Model.Borrow;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IBorrowRequestRepository : IRepositoryBase<BorrowRequest, int>
    {
        Task<BorrowDto> CreateBorrowRequestAsync(CreateBorrowRequest model);

        Task<BorrowDto> UpdateStatusBorrowRequestAsync(UpdateStatusBorrowRequest model);

        Task<PagingResult<BorrowDto>> PagingAsync(string? keyword, string? sortBy, string? orderBy, int pageIndex, int pageSize);
    }
}
