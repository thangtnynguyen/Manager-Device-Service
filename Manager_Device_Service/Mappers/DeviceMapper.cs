using AutoMapper;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Model.Device;
using Manager_Device_Service.Domains.Model.DeviceCategory;
using Manager_Device_Service.Domains.Model.DeviceLog;

namespace Manager_Device_Service.Mappers
{
    public class DeviceMapper : Profile
    {
        public DeviceMapper()
        {
            //Mapper cho device
            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.DeviceCategoryName, opt => opt.MapFrom(src => src.DeviceCategory.Name))
                .ForMember(dest => dest.DeviceCategoryImageUrl, opt => opt.MapFrom(src => src.DeviceCategory.ImageUrl))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room != null ? src.Room.Name : string.Empty));

            CreateMap<CreateDeviceRequest, Device>();

            CreateMap<UpdateDeviceRequest, Device>();

            CreateMap<UpdateStatusDeviceRequest, Device>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.BrokenDate, opt => opt.MapFrom(src => src.BrokenDate));

            //Mapper cho device category
            CreateMap<DeviceCategory, DeviceCategoryDto>();
            CreateMap<CreateDeviceCategoryRequest, DeviceCategory>();
            CreateMap<UpdateDeviceCategoryRequest, DeviceCategory>();

            // Mapping cho device log
            CreateMap<DeviceLog, DeviceLogDto>();
            CreateMap<CreateDeviceLogRequest, DeviceLog>();

        }
    }
}
