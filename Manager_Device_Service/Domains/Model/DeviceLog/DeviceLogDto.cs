using Manager_Device_Service.Domains.Data.Relate_Device;

namespace Manager_Device_Service.Domains.Model.DeviceLog
{
    public class DeviceLogDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DeviceAction Action { get; set; }
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; }
        public int? UserActionId { get; set; }
    }

}
