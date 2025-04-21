namespace Manager_Device_Service.Domains.Model.Building
{
    public class UpdateBuildingRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Description { get; set; }
    }
}
