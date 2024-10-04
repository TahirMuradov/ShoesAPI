using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AllRole")]
    public class DisCountAreaController : ControllerBase
    {
        private readonly  IDisCountAreaService _countAreaService;

        public DisCountAreaController(IDisCountAreaService countAreaService)
        {
            _countAreaService = countAreaService;
        }
        [HttpPost("[action]")]
        public IActionResult AddDiscountArea([FromForm] AddDisCountAreaDTO addDisCountAreaDTO, [FromHeader] string LangCode)
        {
            var result = _countAreaService.AddDiscountArea(addDisCountAreaDTO,LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateDiscountArea([FromForm] UpdateDisCountAreaDTO updateDisCountAreaDTO, [FromHeader] string LangCode)
        {
            var result=_countAreaService.UpdateDisCountArea(updateDisCountAreaDTO,LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
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
        [HttpDelete("[action]")]
        public IActionResult DeleteDisCountArea([FromQuery] Guid Id)
        {
            var result=_countAreaService.Delete(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
