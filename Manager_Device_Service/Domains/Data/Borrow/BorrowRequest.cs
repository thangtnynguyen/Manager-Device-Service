using Manager_Device_Service.Core.Data;
using Manager_Device_Service.Domains.Data.Relate_Device;

namespace Manager_Device_Service.Domains.Data.Borrow
{
    public class BorrowRequest : EntityBase<int>
    {
        // Liên kết đến thiết bị được mượn
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        // Người yêu cầu 
        public int UserActionId { get; set; }

        // Ngày yêu cầu mượn
        public DateTime RequestDate { get; set; } = DateTime.Now;

        // Ngày mượn (khi được duyệt)
        public DateTime? BorrowDate { get; set; }

        // Ngày hạn mượn (với kiểu DateTime, chỉ quan tâm đến ngày, hệ thống dùng Hangfire job nhắc vào 23h59)
        public DateTime? DueDate { get; set; }

        // Trạng thái yêu cầu
        public BorrowRequestStatus Status { get; set; } = BorrowRequestStatus.Pending;

        // Mô tả hoặc ghi chú (tuỳ chọn)
        public string? Description { get; set; }
    }

    public enum BorrowRequestStatus
    {
        Pending,    // Chờ duyệt
        Approved,   // Đã duyệt
        Rejected,   // Từ chối
        Returned    // Đã trả
    }

}
