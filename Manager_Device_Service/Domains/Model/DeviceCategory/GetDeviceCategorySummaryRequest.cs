using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.DeviceCategory
{
    public class GetDeviceCategorySummaryRequest : PagingRequest
    {
        public int RoomId { get; set; } = 0;
    }
}
