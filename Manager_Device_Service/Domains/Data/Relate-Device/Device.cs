using Manager_Device_Service.Core.Data;
using Manager_Device_Service.Domains.Data.University;

namespace Manager_Device_Service.Domains.Data.Relate_Device
{
    public class Device: EntityBase<int>
    {
        public int DeviceCategoryId { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DeviceStatus Status { get; set; } = DeviceStatus.Available;

        // Vị trí hiện tại của thiết bị
        public int? RoomId { get; set; }



        // Ngày mua và hạn bảo hành
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? BrokenDate { get; set; }


        public Room? Room { get; set; }
        public ICollection<DeviceLog> DeviceLogs { get; set; } = new List<DeviceLog>();
        public DeviceCategory DeviceCategory { get; set; }

    }


    public enum DeviceStatus
    {
        Available,    // Sẵn sàng sử dụng
        Using,        // Đang sử dụng
        Borrowed,     // Đang được mượn
        UnderRepair,  // Đang được sửa chữa
        Broken        // Hỏng, không thể sử dụng
    }

}
