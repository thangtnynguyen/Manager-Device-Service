using System.Text.Json.Serialization;

namespace Manager_Device_Service.Domains.Model.DeviceCategory
{
    public class CreateDeviceCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }

        [JsonIgnore]
        public string? ImageUrl { get; set; }
    }

}
