using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.DeviceCategory;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Manager_Device_Service.Domains;
using Manager_Device_Service.Domains.Model.Device;
using System.Data.Entity;

namespace Manager_Device_Service.Repositories.Implement
{
    public class DeviceCategoryRepository : RepositoryBase<DeviceCategory, int>, IDeviceCategoryRepository
    {
        private readonly IMapper _mapper;
        public DeviceCategoryRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<DeviceCategoryDto> CreateDeviceCategoryAsync(CreateDeviceCategoryRequest model)
        {
            await CreateAsync(_mapper.Map<DeviceCategory>(model));
            return _mapper.Map<DeviceCategoryDto>(model);
        }

        public async Task<PagingResult<DeviceCategoryDto>> PagingAsync(string? name, string? sortBy, string? orderBy, int pageIndex, int pageSize)
        {
            var query = _dbContext.DeviceCategories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                string lowerName = name.ToLower();
                query = query.Where(dc => dc.Name.ToLower().Contains(lowerName));
            }

            int total = await query.CountAsync();

            if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(dc => dc.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                query = sortBy == SortByConstant.Asc ? query.OrderBy(dc => dc.Id) : query.OrderByDescending(dc => dc.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(dc => dc.Id);
            }
            else
            {
                // Ví dụ: nếu orderBy là Id
                if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(dc => dc.Id);
                else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(dc => dc.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var data = await _mapper.ProjectTo<DeviceCategoryDto>(query).ToListAsync();

            return new PagingResult<DeviceCategoryDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }
    }
}
