using Manager_Device_Service.Core.Model;

namespace Manager_Device_Service.Domains.Model.Room
{
    public class GetRoomRequest : PagingRequest
    {
        public string? Name { get; set; }
        public int? FloorId { get; set; }
        public int? BuildingId { get; set; }
    }
}
