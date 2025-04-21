using Manager_Device_Service.Domains.Model.Room;

namespace Manager_Device_Service.Domains.Model.Floor
{
    public class FloorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int BuildingId { get; set; }
        public string? Description { get; set; }

        // Thông tin phòng
        public List<RoomDto>? Rooms { get; set; }
    }
}
