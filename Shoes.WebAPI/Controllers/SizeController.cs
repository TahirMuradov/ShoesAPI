using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.SizeDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
       private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpPost("[action]")]
        public IActionResult AddSize([FromBody] AddSizeDTO addSize,[FromHeader]string LangCode)
        {
            var result=_sizeService.AddSize(addSize, LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateSize([FromBody] UpdateSizeDTO UpdateSize, [FromHeader] string LangCode)
        {
            var result = _sizeService.UpdateSize(UpdateSize, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest();
        }
        [HttpDelete("[action]")]
        public IActionResult DeleteSize([FromQuery] Guid Id)
        {
            var result = _sizeService.DeleteSize(Id);
            return result.IsSuccess ? Ok(result) : BadRequest();
        }
        [HttpGet("[action]")]
        public IActionResult GetSize([FromQuery] Guid Id)
        {
            var result = _sizeService.GetSize(Id);
            return result.IsSuccess ? Ok(result) : BadRequest();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSize([FromQuery] int page)
        {
            var result = await _sizeService.GetAllSizeAsync(page);
            return result.IsSuccess ? Ok(result) : BadRequest();
        }
    }
}
