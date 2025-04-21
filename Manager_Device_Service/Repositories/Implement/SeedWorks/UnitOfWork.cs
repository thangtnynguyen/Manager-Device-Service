using AutoMapper;
using Manager_Device_Service.Domains;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Repositories.Implement;
using Microsoft.AspNetCore.Identity;

namespace Manager_Device_Service.Repositories.Interface.ISeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManagerDeviceContext _context;

        public UnitOfWork(ManagerDeviceContext context, IMapper mapper, UserManager<User> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            Buildings = new BuildingRepository(context, mapper,httpContextAccessor);
            Floors = new FloorRepository(context, mapper,httpContextAccessor);
            Rooms = new RoomRepository(context, mapper,httpContextAccessor);
            DeviceCategories = new DeviceCategoryRepository(context, mapper,httpContextAccessor);
            DeviceLogs = new DeviceLogRepository(context, mapper,httpContextAccessor);
            Devices = new DeviceRepository(context, mapper,httpContextAccessor);
            BorrowRequests= new BorrowRequestRepository(context, mapper,httpContextAccessor);
            AccountRequests = new AccountRequestRepository(context, mapper, httpContextAccessor);
            //common
            Mapper = mapper;
        }

        public IBuildingRepository Buildings { get; private set; }

        public IFloorRepository Floors { get; }

        public IRoomRepository Rooms { get; }

        public IDeviceCategoryRepository DeviceCategories { get; }

        public IDeviceLogRepository DeviceLogs { get; }

        public IDeviceRepository Devices { get; }

        public IBorrowRequestRepository BorrowRequests { get; }

        public IAccountRequestRepository AccountRequests { get; }

        //common
        public IMapper Mapper { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
