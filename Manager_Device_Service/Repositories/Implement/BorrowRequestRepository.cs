using AutoMapper;
using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Model.Borrow;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains;
using System.Data.Entity;

namespace Manager_Device_Service.Repositories.Implement
{
    public class BorrowRequestRepository : RepositoryBase<BorrowRequest, int>, IBorrowRequestRepository
    {
        private readonly IMapper _mapper;

        public BorrowRequestRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<BorrowDto> CreateBorrowRequestAsync(CreateBorrowRequest model)
        {
            await CreateAsync(_mapper.Map<BorrowRequest>(model));
            return _mapper.Map<BorrowDto>(model);
        }

        public async Task<BorrowDto> UpdateStatusBorrowRequestAsync(UpdateStatusBorrowRequest model)
        {
            var borrowEntity = await _dbContext.BorrowRequests.FindAsync(model.Id);
            if (borrowEntity == null)
            {
                throw new System.Exception("Borrow request not found");
            }
            _mapper.Map(model, borrowEntity);
            await UpdateAsync(borrowEntity);
            return _mapper.Map<BorrowDto>(borrowEntity);
        }

        public async Task<PagingResult<BorrowDto>> PagingAsync(string? keyword, string? sortBy, string? orderBy, int pageIndex, int pageSize)
        {
            var query = _dbContext.BorrowRequests.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                string lowerKeyword = keyword.ToLower();
                query = query.Where(br => br.Description.ToLower().Contains(lowerKeyword)
                                          || br.Device.SerialNumber.ToLower().Contains(lowerKeyword)
                                          || br.Device.Name.ToLower().Contains(lowerKeyword));
            }

            int total = await query.CountAsync();

            if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(br => br.Id);
            }
            else if (string.IsNullOrEmpty(orderBy))
            {
                query = sortBy == SortByConstant.Asc ? query.OrderBy(br => br.Id) : query.OrderByDescending(br => br.Id);
            }
            else if (string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderByDescending(br => br.Id);
            }
            else
            {
                if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
                    query = query.OrderBy(br => br.Id);
                else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
                    query = query.OrderByDescending(br => br.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var data = await _mapper.ProjectTo<BorrowDto>(query).ToListAsync();
            return new PagingResult<BorrowDto>(data, pageIndex, pageSize, sortBy, orderBy, total);
        }
    }
}
