using Manager_Device_Service.Domains.Model.Floor;

namespace Manager_Device_Service.Domains.Model.Building
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Description { get; set; }

        //Load kèm danh sách tầng
        public List<FloorDto>? Floors { get; set; }
    }
}
