using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.DeviceLog;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;

namespace Manager_Device_Service.Repositories.Interface
{
    public interface IDeviceLogRepository : IRepositoryBase<DeviceLog, int>
    {
        Task<PagingResult<DeviceLogDto>> PagingAsync(int? deviceId, int? userActionId, DeviceAction? action, string? sortBy, string? orderBy, int pageIndex, int pageSize);

        Task<DeviceLogDto> CreateDeviceLogAsync(CreateDeviceLogRequest deviceLog);
    }
}
