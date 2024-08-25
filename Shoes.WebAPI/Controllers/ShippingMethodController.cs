using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.ShippingMethodDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethodService _shippingMethodService;

        public ShippingMethodController(IShippingMethodService shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }
        [HttpPost("[action]")]
        public IActionResult AddShippingMethod([FromBody]AddShippingMethodDTO addShipping,[FromHeader] string LangCode)
        {
            var result=_shippingMethodService.AddShippingMethod(addShipping, LangCode); 
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateShippingMethod([FromBody]UpdateShippingMethodDTO updateShipping,[FromHeader] string LangCode)
        {
            var result = _shippingMethodService.UpdateShippingMethod(updateShipping, LangCode);
            return result.IsSuccess ? Ok(result):BadRequest(result);    
        }
        [HttpDelete("[action]")]
        public IActionResult DeleteShippingMethod([FromQuery]Guid Id)
        {
            var result = _shippingMethodService.DeleteShippingMethod(Id);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetShippingMethod([FromQuery]Guid Id, [FromHeader]string LangCode)
        {
            var result = _shippingMethodService.GetShippingMethod(Id, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllShippingMethodAsync([FromHeader]string LangCode,[FromQuery] int page = 1)
        {
            var result=await _shippingMethodService.GetAllShippingMethodAsync(LangCode, page);
            return result.IsSuccess? Ok(result) :BadRequest(result);
        }
    }
}
