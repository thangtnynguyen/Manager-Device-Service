using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.DeviceCategory;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IDeviceCategoryRepository : IRepositoryBase<DeviceCategory, int>
    {
        Task<PagingResult<DeviceCategoryDto>> PagingAsync(string? name, string? sortBy, string? orderBy, int pageIndex, int pageSize);

        Task<DeviceCategoryDto> CreateDeviceCategoryAsync(CreateDeviceCategoryRequest deviceCategory);

    }
}
