using Manager_Device_Service.Domains.Data.Relate_Device;

namespace Manager_Device_Service.Domains.Model.Device
{
    public class UpdateStatusDeviceRequest
    {
        public int Id { get; set; }
        public DeviceStatus Status { get; set; }
        public DateTime? BrokenDate { get; set; }
    }

}
