using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Room;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IRoomRepository : IRepositoryBase<Room, int>
    {
        Task<PagingResult<RoomDto>> PagingAsync(string? name, int? floorId, int? buildingId, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10);
        Task<RoomDto> CreateRoomAsync(CreateRoomRequest room);
    }
}
