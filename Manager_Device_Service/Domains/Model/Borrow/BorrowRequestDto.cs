using Manager_Device_Service.Domains.Data.Borrow;

namespace Manager_Device_Service.Domains.Model.Borrow
{
    public class BorrowRequestDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceCategoryImageUrl { get; set; }

        public int UserActionId { get; set; }
        public string? Class { get; set; }

        public int? RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? BuildingName { get; set; }
        public string? FloorName { get; set; }
        public string? UserActionName { get; set; }


        public DateTime RequestDate { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
        public BorrowRequestStatus Status { get; set; }
        public string? Description { get; set; }

        public string? BuildingOldPutName { get; set; }
        public string? FloorOldPutName { get; set; }
        public string? RoomOldPutName { get; set; }

        public string DeviceSerialNumber { get; set; } = string.Empty;

    }
}
