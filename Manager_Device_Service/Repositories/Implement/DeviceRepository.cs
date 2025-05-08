using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.Device;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Manager_Device_Service.Domains;
using Manager_Device_Service.Domains.Model.DeviceCategory;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Manager_Device_Service.Repositories.Implement
{
    public class DeviceRepository : RepositoryBase<Device, int>, IDeviceRepository
    {
        private readonly IMapper _mapper;
        public DeviceRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<DeviceDto> CreateDeviceAsync(CreateDeviceRequest model)
        {
            var result = await CreateAsync(_mapper.Map<Device>(model));
            return _mapper.Map<DeviceDto>(result);
        }

        public async Task<DeviceDto> UpdateDeviceAsync(UpdateDeviceRequest model)
        {
            var deviceEntity = await _dbContext.Devices.FindAsync(model.Id);
            if (deviceEntity == null)
            {
                throw new Exception("Device not found.");
            }
            await UpdateAsync(_mapper.Map<Device>(model));
            return _mapper.Map<DeviceDto>(deviceEntity);
        }

        public async Task<DeviceDto> UpdateStatusDeviceAsync(UpdateStatusDeviceRequest model)
        {
            var deviceEntity = await _dbContext.Devices.FindAsync(model.Id);
            if (deviceEntity == null)
            {
                throw new Exception("Device not found.");
            }
            await UpdateAsync(_mapper.Map<Device>(model));
            return _mapper.Map<DeviceDto>(deviceEntity);
        }


        public async Task<PagingResult<DeviceDto>> PagingAsync(string? name, int? deviceCategoryId, int? roomId, string? serialNumber, string? keyword, DeviceStatus? status, string? sortBy, string? orderBy, int pageIndex, int pageSize)
        {
            //var query = _dbContext.Devices.Include(d=>d.DeviceCategory).Include(d=>d.Room).AsQueryable();
            var query = _dbContext.Devices.Where(d => d.IsDeleted != true).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                string lowerName = name.ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(lowerName));
            }
            if (!string.IsNullOrEmpty(serialNumber))
            {
                string lowerSerialNumber = serialNumber.ToLower();
                query = query.Where(d => d.SerialNumber.ToLower().Contains(lowerSerialNumber));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                string lowerKeyword = keyword.ToLower();
                query = query.Where(d => d.SerialNumber.ToLower().Contains(lowerKeyword) || d.Name.ToLower().Contains(lowerKeyword) || d.Description!.ToLower().Contains(lowerKeyword));
            }

            if (deviceCategoryId.HasValue)
            {
                query = query.Where(d => d.DeviceCategoryId == deviceCategoryId.Value);
            }

            if (roomId.HasValue)
            {
                query = query.Where(d => d.RoomId.HasValue && d.RoomId.Value == roomId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(d => d.Status == status.Value);
            }

            int total = await query.CountAsync();

            if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(d => d.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                query = sortBy == SortByConstant.Asc ? query.OrderBy(d => d.Id) : query.OrderByDescending(d => d.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(d => d.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(d => d.Id);
                else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(d => d.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var data = await _mapper.ProjectTo<DeviceDto>(query).ToListAsync();

            return new PagingResult<DeviceDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }


        public async Task<DeviceByIdDto> GetByIdDetailAsync(int id)
        {
            var query = _dbContext.Devices.Where(d => d.Id == id).AsQueryable();

            var device = await _mapper.ProjectTo<DeviceByIdDto>(query).FirstOrDefaultAsync();

            return _mapper.Map<DeviceByIdDto>(device);

        }
    }
}
