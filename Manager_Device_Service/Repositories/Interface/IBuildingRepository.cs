using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Building;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IBuildingRepository : IRepositoryBase<Building, int>
    {
        Task<PagingResult<BuildingDto>> PagingAsync(string? name, string? address, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10);
        Task<BuildingDto> CreateBuildingAsync(CreateBuildingRequest building);
    }
}
