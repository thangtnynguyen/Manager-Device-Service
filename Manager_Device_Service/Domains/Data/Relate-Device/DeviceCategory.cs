using Manager_Device_Service.Core.Data;

namespace Manager_Device_Service.Domains.Data.Relate_Device
{
    public class DeviceCategory:EntityBase<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<Device> Devices { get; set; } = new List<Device>();

    }
}
