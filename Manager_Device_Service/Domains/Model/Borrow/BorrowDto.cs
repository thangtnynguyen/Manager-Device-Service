using Manager_Device_Service.Domains.Data.Borrow;

namespace Manager_Device_Service.Domains.Model.Borrow
{
    public class BorrowDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string? DeviceName { get; set; }

        public int UserActionId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
        public BorrowRequestStatus Status { get; set; }
        public string? Description { get; set; }
    }
}
