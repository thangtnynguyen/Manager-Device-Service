namespace Manager_Device_Service.Domains.Model.Borrow
{
    public class CreateBorrowRequest
    {
        // ID của thiết bị cần mượn
        public int DeviceId { get; set; }

        // ID của người yêu cầu mượn
        public int UserActionId { get; set; }

        // Ngày dự kiến mượn (nếu muốn cho phép người dùng đặt trước)
        // Nếu để trống thì hệ thống sẽ sử dụng RequestDate làm ngày mượn
        public DateTime? BorrowDate { get; set; }

        // Ngày hạn mượn (chỉ quan tâm đến ngày)
        public DateTime DueDate { get; set; }

        // Mô tả hoặc ghi chú (tuỳ chọn)
        public string? Description { get; set; }
    }
}
