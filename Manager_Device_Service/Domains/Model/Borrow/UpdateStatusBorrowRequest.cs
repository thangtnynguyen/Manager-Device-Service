using Manager_Device_Service.Domains.Data.Borrow;

namespace Manager_Device_Service.Domains.Model.Borrow
{
    public class UpdateStatusBorrowRequest
    {
        // Xác định yêu cầu mượn cần cập nhật trạng thái
        public int Id { get; set; }

        // Trạng thái mới: Approved, Rejected, Returned, ...
        public BorrowRequestStatus Status { get; set; }

        //public int? RoomId { get; set; }

        // Nếu duyệt mượn, có thể cập nhật BorrowDate theo thời gian duyệt
        //public DateTime? BorrowDate { get; set; }

        // Nếu trả, có thể ghi chú thêm
        public string? Description { get; set; }
    }
}
