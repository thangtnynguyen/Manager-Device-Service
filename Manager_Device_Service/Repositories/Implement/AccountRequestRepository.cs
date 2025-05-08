using AutoMapper;
using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.User;
using Manager_Device_Service.Domains.Model.AccountRequest;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Repositories.Interface;
using System;
using Manager_Device_Service.Domains;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Manager_Device_Service.Repositories.Implement
{
    public class AccountRequestRepository : RepositoryBase<AccountRequest, int>, IAccountRequestRepository
    {
        private readonly ManagerDeviceContext _context;
        private readonly IMapper _mapper;

        public AccountRequestRepository(ManagerDeviceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagingResult<AccountRequestDto>> GetPagingAsync(string? keyword, AccountRequestStatus? status, string? sortBy, string? orderBy, int pageIndex, int pageSize)
        {
            var query = _context.AccountRequests.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x =>
                    x.Code.Contains(keyword) ||
                    x.Email.Contains(keyword) ||
                    x.PhoneNumber.Contains(keyword) ||
                    x.ClassCode.Contains(keyword));
            }

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            // Optional sort logic (extendable)
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("RequestDate", StringComparison.OrdinalIgnoreCase))
                {
                    query = orderBy?.ToLower() == "desc" ? query.OrderByDescending(x => x.RequestDate) : query.OrderBy(x => x.RequestDate);
                }
            }

            var total = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<AccountRequestDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagingResult<AccountRequestDto>(items, pageIndex, pageSize, total);
        }

        public async Task<AccountRequestDto> CreateAccountRequestAsync(CreateAccountRequest request)
        {
            var entity = _mapper.Map<AccountRequest>(request);
            await CreateAsync(entity);
            return _mapper.Map<AccountRequestDto>(entity);
        }

        public async Task<AccountRequestDto> UpdateStatusAsync(UpdateAccountRequestStatus request)
        {
            var entity = await _context.AccountRequests.FindAsync(request.Id);
            if (entity == null) throw new Exception("Yêu cầu không tồn tại");

            entity.Status = request.Status;
            await UpdateAsync(entity);


            return _mapper.Map<AccountRequestDto>(entity);
        }
    }
}
