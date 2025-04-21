namespace Manager_Device_Service.Domains.Model.Floor
{
    public class CreateFloorRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int BuildingId { get; set; }
        public string? Description { get; set; }
    }
}
