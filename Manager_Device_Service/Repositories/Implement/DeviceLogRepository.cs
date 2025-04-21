using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.DeviceLog;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Manager_Device_Service.Domains;
using System.Data.Entity;

namespace Manager_Device_Service.Repositories.Implement
{
    public class DeviceLogRepository : RepositoryBase<DeviceLog, int>, IDeviceLogRepository
    {
        private readonly IMapper _mapper;
        public DeviceLogRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<DeviceLogDto> CreateDeviceLogAsync(CreateDeviceLogRequest deviceLog)
        {
            await CreateAsync(_mapper.Map<DeviceLog>(deviceLog));
            return _mapper.Map<DeviceLogDto>(deviceLog);
        }

        public async Task<PagingResult<DeviceLogDto>> PagingAsync(int? deviceId, int? userActionId, DeviceAction? action, string? sortBy, string? orderBy, int pageIndex, int pageSize)
        {
            var query = _dbContext.DeviceLogs.AsQueryable();

            if (deviceId.HasValue)
                query = query.Where(dl => dl.DeviceId == deviceId.Value);

            if (userActionId.HasValue)
                query = query.Where(dl => dl.UserActionId != null && dl.UserActionId == userActionId.Value);

            if (action.HasValue)
                query = query.Where(dl => dl.Action == action.Value);

            int total = await query.CountAsync();

            if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(dl => dl.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                query = sortBy == SortByConstant.Asc ? query.OrderBy(dl => dl.Id) : query.OrderByDescending(dl => dl.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(dl => dl.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(dl => dl.Id);
                else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(dl => dl.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var data = await _mapper.ProjectTo<DeviceLogDto>(query).ToListAsync();

            return new PagingResult<DeviceLogDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }
    }
}
