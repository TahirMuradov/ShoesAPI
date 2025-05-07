using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeSliderItemController : ControllerBase
    {
        private readonly IHomeSliderItemService _homeSliderItemService;

        public HomeSliderItemController(IHomeSliderItemService homeSliderItemService)
        {
            _homeSliderItemService = homeSliderItemService;
        }
        [Authorize(Policy = "AllRole")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddHomeSliderItem([FromForm] AddHomeSliderItemDTO addHomeSliderItemDTO, [FromHeader] string LangCode)
        {
            var result = await _homeSliderItemService.AddHomeSliderItemAsync(addHomeSliderItemDTO, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateHomeSliderItem([FromForm] UpdateHomeSliderItemDTO updateHomeSliderItemDTO, [FromHeader] string LangCode)
        {
            var result = await _homeSliderItemService.UpdateHomeSliderItemAsync(updateHomeSliderItemDTO, culture: LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpDelete("[action]")]
        public IActionResult DeleteHomeSliderItem([FromQuery] Guid Id)
        {
            var result = _homeSliderItemService.DeleteHomeSliderItem(Id);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        [Authorize(Policy = "AllRole")]
        public async Task<IActionResult> GetAllHomeSliderItem([FromHeader] string LangCode, [FromQuery] int page)
        {
            var result = await _homeSliderItemService.GetAllHomeSliderAsync(LangCode, page);
            return StatusCode((int)result.StatusCode, result);

        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public IActionResult GetHomeSliderItemForUpdate([FromQuery] Guid Id)
        {
            var result = _homeSliderItemService.GetHomeSliderItemForUpdate(Id);

            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
     
        public IActionResult GetHomeSliderItemForUI([FromHeader] string LangCode)
        {
            var result = _homeSliderItemService.GetHomeSliderItemForUI(LangCode);
            return StatusCode((int)result.StatusCode, result);
        }



    }
}
