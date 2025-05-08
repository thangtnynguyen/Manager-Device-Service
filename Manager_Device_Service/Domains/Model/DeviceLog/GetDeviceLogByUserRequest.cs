using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;

namespace Manager_Device_Service.Domains.Model.DeviceLog
{
    public class GetDeviceLogByUserRequest:PagingRequest
    {
        public int UserActionId { get; set; }
        public int? DeviceId { get; set; }
        public DeviceAction? Action { get; set; }
    }
}
