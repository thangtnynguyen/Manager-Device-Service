using AutoMapper;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Building;
using Manager_Device_Service.Domains.Model.Floor;
using Manager_Device_Service.Domains.Model.Room;

namespace Manager_Device_Service.Mappers
{
    public class BuildingMapper: Profile
    {
        public BuildingMapper() {

            // Mapping cho Building
            CreateMap<Building, BuildingDto>()
                .ForMember(dest => dest.Floors, opt => opt.MapFrom(src => src.Floors));
            CreateMap<CreateBuildingRequest, Building>();
            CreateMap<UpdateBuildingRequest, Building>();

            // Mapping cho Floor
            CreateMap<Floor, FloorDto>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
            CreateMap<CreateFloorRequest, Floor>();
            CreateMap<UpdateFloorRequest, Floor>();

            // Mapping cho Room
            CreateMap<Room, RoomDto>();
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>();
        }
    }
}
