using Manager_Device_Service.Core.Data;

namespace Manager_Device_Service.Domains.Data.Relate_Device
{
    public class DeviceLog:EntityBase<int>
    {
        public int DeviceId { get; set; }
        public DeviceAction Action { get; set; }
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public int? UserActionId { get; set; }

        public Device Device { get; set; }

    }

    public enum DeviceAction
    {
        Borrowed,
        Returned,
        Moved,
        Repaired
    }

}
