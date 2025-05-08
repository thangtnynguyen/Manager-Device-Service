using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Floor;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Manager_Device_Service.Domains;
using Microsoft.EntityFrameworkCore;

namespace Manager_Device_Service.Repositories.Implement
{
    public class FloorRepository : RepositoryBase<Floor, int>, IFloorRepository
    {
        private readonly IMapper _mapper;

        public FloorRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<FloorDto> CreateFloorAsync(CreateFloorRequest floor)
        {
            _dbContext.Floors.Add(_mapper.Map<Floor>(floor));
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<FloorDto>(floor);
        }

        public async Task<PagingResult<FloorDto>> PagingAsync(string? name, int? buildingId, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10)
        {
            var query = _dbContext.Floors.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                string searchName = name.ToLower();
                query = query.Where(f => f.Name.ToLower().Contains(searchName));
            }

            if (buildingId.HasValue)
            {
                query = query.Where(f => f.BuildingId == buildingId.Value);
            }

            int total = await query.CountAsync();

            if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(f => f.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                if (sortBy == SortByConstant.Asc)
                    query = query.OrderBy(f => f.Id);
                else
                    query = query.OrderByDescending(f => f.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(f => f.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(f => f.Id);
                else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(f => f.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var data = await _mapper.ProjectTo<FloorDto>(query).ToListAsync();
            return new PagingResult<FloorDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }
    }
}
