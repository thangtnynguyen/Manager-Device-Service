using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Building;
using Manager_Device_Service.Domains.Model.Floor;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/floor")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FloorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/floor/get-by-id?id=1
        [HttpGet]
        [Route("get-by-id")]
        public async Task<ApiResult<FloorDto>> GetById([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _unitOfWork.Floors.GetByIdAsync(request.Id);
            var resultDto = _unitOfWork.Mapper.Map<FloorDto>(result);
            return ApiResult<FloorDto>.Success("Thành công", resultDto);
        }

        // GET: api/floor/paging?Name=...&BuildingId=1&PageIndex=1&PageSize=10
        [HttpGet]
        [Route("paging")]
        public async Task<ApiResult<PagingResult<FloorDto>>> Paging([FromQuery] GetFloorRequest request)
        {
            var result = await _unitOfWork.Floors.PagingAsync(
                request.Name,
                request.BuildingId,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<FloorDto>>.Success("Thành công", result);
        }

        // POST: api/floor/create
        [HttpPost]
        [Route("create")]
        public async Task<ApiResult<FloorDto>> Create([FromBody] CreateFloorRequest request)
        {
            var result = await _unitOfWork.Floors.CreateFloorAsync(request);
            return ApiResult<FloorDto>.Success("Tạo tầng thành công", result);
        }
    }
}
