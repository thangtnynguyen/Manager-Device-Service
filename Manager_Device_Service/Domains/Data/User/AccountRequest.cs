using Manager_Device_Service.Core.Data;

namespace Manager_Device_Service.Domains.Data.User
{
    public class AccountRequest : EntityBase<int>
    {
        // Tên sinh viên hoặc giảng viên
        public string Name { get; set; } = string.Empty;

        // Mã sinh viên hoặc giảng viên
        public string Code { get; set; } = string.Empty;

        // Email người yêu cầu
        public string Email { get; set; } = string.Empty;

        // Số điện thoại liên hệ
        public string PhoneNumber { get; set; } = string.Empty;

        // Mã lớp
        public string ClassCode { get; set; } = string.Empty;

        // Chức vụ hiện tại (Sinh viên / Giảng viên / Khác)
        public string Position { get; set; } = string.Empty;

        // Lý do yêu cầu cấp tài khoản
        public string Reason { get; set; } = string.Empty;

        // Trạng thái yêu cầu (Chưa cấp, Đã cấp, Đã từ chối)
        public AccountRequestStatus Status { get; set; } = AccountRequestStatus.Pending;

        // Ngày gửi yêu cầu
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }

    public enum AccountRequestStatus
    {
        Pending,    // Chưa cấp
        Approved,   // Đã cấp
        Rejected    // Đã từ chối
    }

}
