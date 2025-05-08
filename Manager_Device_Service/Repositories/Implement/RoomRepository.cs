using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Room;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Manager_Device_Service.Domains;
using Microsoft.EntityFrameworkCore;

namespace Manager_Device_Service.Repositories.Implement
{
    public class RoomRepository : RepositoryBase<Room, int>, IRoomRepository
    {
        private readonly IMapper _mapper;

        public RoomRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<RoomDto> CreateRoomAsync(CreateRoomRequest room)
        {
            _dbContext.Rooms.Add(_mapper.Map<Room>(room));
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<PagingResult<RoomDto>> PagingAsync(string? name, int? floorId, int? buildingId, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10)
        {
            var query = _dbContext.Rooms.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                string searchName = name.ToLower();
                query = query.Where(r => r.Name.ToLower().Contains(searchName));
            }

            if (floorId.HasValue)
            {
                query = query.Where(r => r.FloorId == floorId.Value);
            }

            if (buildingId.HasValue)
            {
                query = query.Where(r => r.BuildingId.HasValue && r.BuildingId.Value == buildingId.Value);
            }

            int total = await query.CountAsync();

            if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(r => r.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                if (sortBy == SortByConstant.Asc)
                    query = query.OrderBy(r => r.Id);
                else
                    query = query.OrderByDescending(r => r.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(r => r.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(r => r.Id);
                else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(r => r.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var data = await _mapper.ProjectTo<RoomDto>(query).ToListAsync();
            return new PagingResult<RoomDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }
    }
}
