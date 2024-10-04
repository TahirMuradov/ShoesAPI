using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AllRole")]
    public class TopCategoryAreaController : ControllerBase
    {
        private readonly ITopCategoryAreaService _topCategoryAreaService;

        public TopCategoryAreaController(ITopCategoryAreaService topCategoryAreaService)
        {
            _topCategoryAreaService = topCategoryAreaService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddToPCategoryArea([FromForm] AddTopCategoryAreaDTO addTopCategoryAreaDTO, [FromHeader] string LangCode)
        {
            var result=await _topCategoryAreaService.AddTopCategoryAreaAsync(addTopCategoryAreaDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTopCategoryArea([FromForm] UpdateTopCategoryAreaDTO updateTopCategoryAreaDTO, [FromHeader] string LangCode)
        {
            var result = await _topCategoryAreaService.UpdateTopCategoryAreaAsync(updateTopCategoryAreaDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult RemoveTopCategoryArea([FromQuery]Guid Id)
        {
            var result=_topCategoryAreaService.RemoveTopCategoryArea(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTopCategoryArea([FromQuery] int page, [FromHeader] string LangCode)
        {
            var result = await _topCategoryAreaService.GetTopCategoryAreaAsync(LangCode, page);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetTopCategoryAreaForUI([FromHeader] string LangCode)
        {
            var result=_topCategoryAreaService.GetTopCategoryAreaForUI(LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetTopCategoryAreaForUpdate([FromQuery]Guid Id)
        {
            var result=_topCategoryAreaService.GetTopcategoryAreaForUpdate(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
