namespace Manager_Device_Service.Domains.Model.Room
{
    public class CreateRoomRequest
    {
        public string Name { get; set; } = string.Empty;
        public int FloorId { get; set; }
        // Nếu muốn lưu thông tin Building trực tiếp (tùy chọn)
        public int? BuildingId { get; set; }
        public string? Usage { get; set; }
        public string? Description { get; set; }
    }
}
