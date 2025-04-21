using Manager_Device_Service.Domains.Data.Relate_Device;

namespace Manager_Device_Service.Domains.Model.Device
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public int DeviceCategoryId { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DeviceStatus Status { get; set; }
        public int? RoomId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? BrokenDate { get; set; }

        public string? DeviceCategoryName { get; set; }
        public string? DeviceCategoryImageUrl { get; set; }

        // Thông tin vị trí (Room)
        public string? RoomName { get; set; }
    }

}
