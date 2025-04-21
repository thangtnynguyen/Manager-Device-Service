using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Model.Building;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager_Device_Service.Controllers
{
    [Route("api/building")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BuildingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/building/get-by-id?id=1
        [HttpGet]
        [Route("get-by-id")]
        public async Task<ApiResult<BuildingDto>> GetById([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _unitOfWork.Buildings.GetByIdAsync(request.Id);
            var resultDto =_unitOfWork.Mapper.Map<BuildingDto>(result);
            return ApiResult<BuildingDto>.Success("Thành công", resultDto);
        }

        // GET: api/building/paging?Name=...&Address=...&PageIndex=1&PageSize=10
        [HttpGet]
        [Route("paging")]
        public async Task<ApiResult<PagingResult<BuildingDto>>> Paging([FromQuery] GetBuildingRequest request)
        {
            var result = await _unitOfWork.Buildings.PagingAsync(
                request.Name,
                request.Address,
                request.SortBy,
                request.OrderBy,
                request.PageIndex,
                request.PageSize);
            return ApiResult<PagingResult<BuildingDto>>.Success("Thành công", result);
        }

        // POST: api/building/create
        [HttpPost]
        [Route("create")]
        public async Task<ApiResult<BuildingDto>> Create([FromBody] CreateBuildingRequest request)
        {
            var result = await _unitOfWork.Buildings.CreateBuildingAsync(request);
            return ApiResult<BuildingDto>.Success("Tạo tòa nhà thành công", result);
        }
    }


}
