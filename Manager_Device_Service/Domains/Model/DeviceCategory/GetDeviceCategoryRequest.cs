using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.DeviceCategory
{
    public class GetDeviceCategoryRequest : PagingRequest
    {
        public string? Name { get; set; }
    }
}
