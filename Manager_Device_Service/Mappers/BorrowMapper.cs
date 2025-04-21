using AutoMapper;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains.Model.Borrow;

namespace Manager_Device_Service.Mappers
{
    public class BorrowMapper : Profile
    {
        public BorrowMapper()
        {
            CreateMap<BorrowRequest, BorrowDto>()
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Device != null ? src.Device.Name : string.Empty));

            CreateMap<CreateBorrowRequest, BorrowRequest>()
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BorrowRequestStatus.Pending));

            CreateMap<UpdateStatusBorrowRequest, BorrowRequest>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.BorrowDate, opt => opt.Condition(src => src.BorrowDate.HasValue))
                .ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
