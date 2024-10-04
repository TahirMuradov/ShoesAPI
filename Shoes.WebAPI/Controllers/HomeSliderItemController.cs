using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AllRole")]
    public class HomeSliderItemController : ControllerBase
    {
        private readonly IHomeSliderItemService _homeSliderItemService;

        public HomeSliderItemController(IHomeSliderItemService homeSliderItemService)
        {
            _homeSliderItemService = homeSliderItemService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddHomeSliderItem([FromForm] AddHomeSliderItemDTO addHomeSliderItemDTO, [FromHeader] string LangCode)
        {
            var result = await _homeSliderItemService.AddHomeSliderItemAsync(addHomeSliderItemDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateHomeSliderItem([FromForm] UpdateHomeSliderItemDTO updateHomeSliderItemDTO, [FromHeader] string LangCode)
        {
            var result = await _homeSliderItemService.UpdateHomeSliderItemAsync(updateHomeSliderItemDTO, culture: LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult DeleteHomeSliderItem([FromQuery] Guid Id)
        {
            var result = _homeSliderItemService.DeleteHomeSliderItem(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllHomeSliderItem([FromHeader] string LangCode, [FromQuery] int page)
        {
            var result = await _homeSliderItemService.GetAllHomeSliderAsync(LangCode, page);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [HttpGet("[action]")]
        public IActionResult GetHomeSliderItemForUpdate([FromQuery] Guid Id)
        {
            var result = _homeSliderItemService.GetHomeSliderItemForUpdate(Id);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]

        public IActionResult GetHomeSliderItemForUI([FromHeader] string LangCode)
        {
            var result = _homeSliderItemService.GetHomeSliderItemForUI(LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }



    }
}
