using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class DisCountAreaController : ControllerBase
    {
        private readonly  IDisCountAreaService _countAreaService;

        public DisCountAreaController(IDisCountAreaService countAreaService)
        {
            _countAreaService = countAreaService;
        }
        [Authorize(Policy = "AllRole")]
        [HttpPost("[action]")]
        public IActionResult AddDiscountArea([FromBody] AddDisCountAreaDTO addDisCountAreaDTO, [FromHeader] string LangCode)
        {
            var result = _countAreaService.AddDiscountArea(addDisCountAreaDTO,LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpPut("[action]")]
        public IActionResult UpdateDiscountArea([FromBody] UpdateDisCountAreaDTO updateDisCountAreaDTO, [FromHeader] string LangCode)
        {
            var result=_countAreaService.UpdateDisCountArea(updateDisCountAreaDTO,LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public IActionResult GetDisCountAreaForUpdate([FromQuery] Guid Id)
        {
            var result=_countAreaService.GetDisCountAreaForUpdate(Id);
            return result.IsSuccess ? Ok(result) :BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllDisCountArea([FromHeader] string LangCode)
        {
            var result=_countAreaService.GetAllDisCountArea(LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpDelete("[action]")]
        public IActionResult DeleteDisCountArea([FromQuery] Guid Id)
        {
            var result=_countAreaService.Delete(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
          [HttpGet("[action]")]
        public async Task< IActionResult> GetDisCountAreaForTable([FromQuery] int page, [FromHeader] string LangCode)
        {
            var result=await _countAreaService.GetAllDisCountForTableAsync(LangCode, page);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
