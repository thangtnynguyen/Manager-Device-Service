using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Building;
using Manager_Device_Service.Repositories.Interface;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Manager_Device_Service.Repositories.Implement
{
    public class BuildingRepository: RepositoryBase<Building, int>, IBuildingRepository
    {
        private readonly IMapper _mapper;
        public BuildingRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<BuildingDto> CreateBuildingAsync(CreateBuildingRequest building)
        {
            _dbContext.Buildings.Add(_mapper.Map<Building>(building));
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<BuildingDto>(building);
        }

        public async Task<PagingResult<BuildingDto>> PagingAsync(string? name, string? address, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10)
        {
            var query = _dbContext.Buildings.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                string searchName = name.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(searchName));
            }

            if (!string.IsNullOrEmpty(address))
            {
                string searchAddress = address.ToLower();
                query = query.Where(b => b.Address != null && b.Address.ToLower().Contains(searchAddress));
            }

            int total = await query.CountAsync();

            // Sắp xếp theo mặc định nếu không truyền tham số
            if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                if (sortBy == SortByConstant.Asc)
                    query = query.OrderBy(b => b.Id);
                else
                    query = query.OrderByDescending(b => b.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(b => b.Id);
                else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(b => b.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var data = await _mapper.ProjectTo<BuildingDto>(query).ToListAsync();
            return new PagingResult<BuildingDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }
    }
}

