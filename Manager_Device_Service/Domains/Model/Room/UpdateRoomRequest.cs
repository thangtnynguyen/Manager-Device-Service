namespace Manager_Device_Service.Domains.Model.Room
{
    public class UpdateRoomRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int FloorId { get; set; }
        public int? BuildingId { get; set; }
        public string? Usage { get; set; }
        public string? Description { get; set; }
    }
}
