using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;

namespace Manager_Device_Service.Domains.Model.Device
{
    public class GetDeviceRequest : PagingRequest
    {
        public string? Name { get; set; }
        public int? DeviceCategoryId { get; set; }
        public int? RoomId { get; set; }
        public DeviceStatus? Status { get; set; }
    }
}
