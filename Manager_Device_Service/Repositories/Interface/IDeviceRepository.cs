using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.Device;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IDeviceRepository : IRepositoryBase<Device, int>
    {
        Task<PagingResult<DeviceDto>> PagingAsync(string? name, int? deviceCategoryId, int? roomId, DeviceStatus? status, string? sortBy, string? orderBy, int pageIndex, int pageSize);

        Task<DeviceDto> CreateDeviceAsync(CreateDeviceRequest model);

        Task<DeviceDto> UpdateDeviceAsync(UpdateDeviceRequest model);

        Task<DeviceDto> UpdateStatusDeviceAsync(UpdateStatusDeviceRequest model);
    }
}
