namespace Manager_Device_Service.Domains.Model.DeviceCategory
{
    public class UpdateDeviceCategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

}
