using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Floor;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IFloorRepository : IRepositoryBase<Floor, int>
    {
        Task<PagingResult<FloorDto>> PagingAsync(string? name, int? buildingId, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10);
        Task<FloorDto> CreateFloorAsync(CreateFloorRequest floor);
    }
}
