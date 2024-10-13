using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AllRole")]
        [HttpPost("[action]")]
        public IActionResult AddSize([FromBody] AddSizeDTO addSize,[FromHeader]string LangCode)
        {
            var result=_sizeService.AddSize(addSize, LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpPut("[action]")]
        public IActionResult UpdateSize([FromBody] UpdateSizeDTO UpdateSize, [FromHeader] string LangCode)
        {
            var result = _sizeService.UpdateSize(UpdateSize, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest();
        }
        [Authorize(Policy = "AllRole")]
        [HttpDelete("[action]")]
        public IActionResult DeleteSize([FromQuery] Guid Id)
        {
            var result = _sizeService.DeleteSize(Id);
            return result.IsSuccess ? Ok(result) : BadRequest();
        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public IActionResult GetSize([FromQuery] Guid Id)
        {
            var result = _sizeService.GetSize(Id);
            return result.IsSuccess ? Ok(result) : BadRequest();
        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSizeForTable([FromQuery] int page)
        {
            var result = await _sizeService.GetAllSizeForTableAsync(page);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllSize()
        
        {
            var result = _sizeService.GetAllSize();

            return result.IsSuccess?Ok(result):BadRequest(result);
        }
    }
}
