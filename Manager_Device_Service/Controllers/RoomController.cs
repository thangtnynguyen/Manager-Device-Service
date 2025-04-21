using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Building;
using Manager_Device_Service.Domains.Model.Room;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/room/get-by-id?id=1
        [HttpGet]
        [Route("get-by-id")]
        public async Task<ApiResult<RoomDto>> GetById([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _unitOfWork.Rooms.GetByIdAsync(request.Id);
            var resultDto = _unitOfWork.Mapper.Map<RoomDto>(result);
            return ApiResult<RoomDto>.Success("Thành công", resultDto);
        }

        // GET: api/room/paging?Name=...&FloorId=1&BuildingId=1&PageIndex=1&PageSize=10
        [HttpGet]
        [Route("paging")]
        public async Task<ApiResult<PagingResult<RoomDto>>> Paging([FromQuery] GetRoomRequest request)
        {
            var result = await _unitOfWork.Rooms.PagingAsync(
                request.Name,
                request.FloorId,
                request.BuildingId,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<RoomDto>>.Success("Thành công", result);
        }

        // POST: api/room/create
        [HttpPost]
        [Route("create")]
        public async Task<ApiResult<RoomDto>> Create([FromBody] CreateRoomRequest request)
        {
            var result = await _unitOfWork.Rooms.CreateRoomAsync(request);
            return ApiResult<RoomDto>.Success("Tạo phòng thành công", result);
        }
    }
}
