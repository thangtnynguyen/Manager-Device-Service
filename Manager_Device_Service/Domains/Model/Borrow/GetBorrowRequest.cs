using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Borrow;

namespace Manager_Device_Service.Domains.Model.Borrow
{
    public class GetBorrowRequest:PagingRequest
    {
        public string? Keyword { get; set; }
        public string? Class { get; set; }
        public string? RoomName { get; set; }
        public int? UserActionId { get; set; }


        public BorrowRequestStatus? Status { get; set; }
    }
}
