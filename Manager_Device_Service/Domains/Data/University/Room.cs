using Manager_Device_Service.Core.Data;

namespace Manager_Device_Service.Domains.Data.University
{
    public class Room: EntityBase<int>
    {
        public string Name { get; set; } = string.Empty;

        // Khóa ngoại liên kết đến Floor
        public int FloorId { get; set; }
        public Floor Floor { get; set; }

        // Tuỳ chọn: Nếu muốn lưu trực tiếp thông tin Building
        public int? BuildingId { get; set; }
        public Building? Building { get; set; }

        public string? Usage { get; set; }       // Chức năng sử dụng của phòng
        public string? Description { get; set; }   // Thông tin mô tả thêm
    }
}
