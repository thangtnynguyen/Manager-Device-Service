using AutoMapper;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains.Model.Borrow;

namespace Manager_Device_Service.Mappers
{
    public class BorrowMapper : Profile
    {
        public BorrowMapper()
        {
            CreateMap<BorrowRequest, BorrowRequestDto>()
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Device != null ? src.Device.Name : string.Empty))
                .ForMember(dest => dest.DeviceSerialNumber, opt => opt.MapFrom(src => src.Device != null ? src.Device.SerialNumber : string.Empty))
                .ForMember(dest => dest.UserActionName, opt => opt.MapFrom(src => src.UserAction != null ? src.UserAction.Name : string.Empty))

                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room != null ? src.Room.Name : string.Empty))
                .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Room != null ? src.Room.Floor.Building.Name : string.Empty))
                .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Room != null ? src.Room.Floor.Name : string.Empty))


                .ForMember(dest => dest.RoomOldPutName, opt => opt.MapFrom(src => src.Device.Room != null ? src.Device.Room.Name : string.Empty))
                .ForMember(dest => dest.BuildingOldPutName, opt => opt.MapFrom(src => src.Device.Room != null ? src.Device.Room.Floor.Building.Name : string.Empty))
                .ForMember(dest => dest.FloorOldPutName, opt => opt.MapFrom(src => src.Device.Room != null ? src.Device.Room.Floor.Name : string.Empty))

                .ForMember(dest => dest.DeviceCategoryImageUrl, opt => opt.MapFrom(src => src.Device.DeviceCategory != null ? src.Device.DeviceCategory.ImageUrl : string.Empty));



            CreateMap<CreateBorrowRequest, BorrowRequest>()
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BorrowRequestStatus.Pending));

            CreateMap<UpdateStatusBorrowRequest, BorrowRequest>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                //.ForMember(dest => dest.BorrowDate, opt => opt.Condition(src => src.BorrowDate.HasValue))
                //.ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<CreateBorrowRequest, BorrowRequestDto>();

        }
    }
}
