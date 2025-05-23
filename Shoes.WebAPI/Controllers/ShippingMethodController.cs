﻿using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AllRole")]
        [HttpPost("[action]")]
        public IActionResult AddShippingMethod([FromBody]AddShippingMethodDTO addShipping,[FromHeader] string LangCode)
        {           
            var result=_shippingMethodService.AddShippingMethod(addShipping, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpPut("[action]")]
        public IActionResult UpdateShippingMethod([FromBody]UpdateShippingMethodDTO updateShipping,[FromHeader] string LangCode)
        {
            var result = _shippingMethodService.UpdateShippingMethod(updateShipping, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpDelete("[action]")]
        public IActionResult DeleteShippingMethod([FromQuery]Guid Id)
        {
            var result = _shippingMethodService.DeleteShippingMethod(Id);
            return StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public IActionResult GetShippingMethodForUpdate([FromQuery]Guid Id) 
        {
            var result=_shippingMethodService.GetShippingMethodForUpdate(Id);
            return StatusCode((int)result.StatusCode, result);

        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public IActionResult GetShippingMethod([FromQuery]Guid Id, [FromHeader]string LangCode)
        {
            var result = _shippingMethodService.GetShippingMethod(Id, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllShippingMethodAsync([FromHeader]string LangCode,[FromQuery] int page = 1)
        {
            var result=await _shippingMethodService.GetAllShippingMethodAsync(LangCode, page);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        public IActionResult GetShippingMethodForUI([FromHeader] string LangCode)
        {
            var result=_shippingMethodService.GetShippingMethodForUI(LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
