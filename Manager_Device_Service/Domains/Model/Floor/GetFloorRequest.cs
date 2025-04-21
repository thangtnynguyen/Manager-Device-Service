using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.Floor
{
    public class GetFloorRequest : PagingRequest
    {
        public string? Name { get; set; }
        public int? BuildingId { get; set; }
    }
}
