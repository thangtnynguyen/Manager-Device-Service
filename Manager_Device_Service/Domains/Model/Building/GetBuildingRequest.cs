using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.Building
{
    public class GetBuildingRequest : PagingRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
