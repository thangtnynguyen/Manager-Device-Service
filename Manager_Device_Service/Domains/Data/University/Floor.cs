using Manager_Device_Service.Core.Data;

namespace Manager_Device_Service.Domains.Data.University
{
    public class Floor: EntityBase<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }  // Số thứ tự của tầng

        // Khóa ngoại liên kết đến Building
        public int BuildingId { get; set; }
        public string? Description { get; set; }
        public Building Building { get; set; }

        // Một tầng có nhiều phòng
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

    }
}
