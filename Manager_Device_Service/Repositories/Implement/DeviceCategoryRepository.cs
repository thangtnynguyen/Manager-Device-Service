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
using Microsoft.EntityFrameworkCore;

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
            var query = _dbContext.DeviceCategories.Where(d => d.IsDeleted != true).AsQueryable();

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


        public async Task<PagingResult<DeviceCategorySummaryDto>> PagingCategorySummaryByRoomAsync(int roomId, string? sortBy, string? orderBy, int pageIndex, int pageSize)
        {
            var query = _dbContext.Devices
                .Where(d => d.RoomId == roomId && d.IsDeleted != true)
                .GroupBy(d => new { d.DeviceCategoryId, d.DeviceCategory.Name })
                .Select(g => new DeviceCategorySummaryDto
                {
                    Id = g.Key.DeviceCategoryId,
                    Name = g.Key.Name,
                    Description=g.FirstOrDefault().DeviceCategory.Description,
                    ImageUrl = g.FirstOrDefault().DeviceCategory.ImageUrl,  
                    Quantity = g.Count()
                })
                .Where(g => g.Quantity > 0);

            // Sorting
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderByDescending(x => x.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                query = sortBy == SortByConstant.Asc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(x => x.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Name && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(x => x.Name);
                else if (orderBy == OrderByConstant.Name && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(x => x.Name);
                else if (orderBy == OrderByConstant.Quantity && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(x => x.Quantity);
                else if (orderBy == OrderByConstant.Quantity && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(x => x.Quantity);
                else
                    query = query.OrderByDescending(x => x.Id); // fallback
            }

            int total = await query.CountAsync();

            var data = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<DeviceCategorySummaryDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }


        public async Task<PagingResult<DeviceCategorySummaryDto>> PagingCategorySummaryAsync(string? sortBy, string? orderBy, int pageIndex, int pageSize)
        {
            var query = _dbContext.Devices
                .Where(d => d.IsDeleted != true)
                .GroupBy(d => new { d.DeviceCategoryId, d.DeviceCategory.Name })
                .Select(g => new DeviceCategorySummaryDto
                {
                    Id = g.Key.DeviceCategoryId,
                    Name = g.Key.Name,
                    Description = g.FirstOrDefault().DeviceCategory.Description,
                    ImageUrl = g.FirstOrDefault().DeviceCategory.ImageUrl,
                    Quantity = g.Count()
                })
                .Where(g => g.Quantity > 0);

            // Sorting
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderByDescending(x => x.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                query = sortBy == SortByConstant.Asc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(x => x.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Name && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(x => x.Name);
                else if (orderBy == OrderByConstant.Name && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(x => x.Name);
                else if (orderBy == OrderByConstant.Quantity && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(x => x.Quantity);
                else if (orderBy == OrderByConstant.Quantity && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(x => x.Quantity);
                else
                    query = query.OrderByDescending(x => x.Id); // fallback
            }

            int total = await query.CountAsync();

            var data = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<DeviceCategorySummaryDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }



    }



}
