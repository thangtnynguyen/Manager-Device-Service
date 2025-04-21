using AutoMapper;
using Manager_Device_Service.Repositories.Implement;

namespace Manager_Device_Service.Repositories.Interface.ISeedWorks
{
    public interface IUnitOfWork
    {
        public IBuildingRepository Buildings { get; }

        public IFloorRepository Floors { get; }

        public IRoomRepository Rooms { get; }

        public IDeviceCategoryRepository DeviceCategories { get; }

        public IDeviceLogRepository DeviceLogs { get; }

        public IDeviceRepository Devices { get; }

        public IBorrowRequestRepository BorrowRequests { get; }

        public IAccountRequestRepository AccountRequests { get; }


        //common
        public IMapper Mapper { get; }
        Task<int> CompleteAsync();
    }
}
