using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.CuponDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponService _cuponService;

        public CuponController(ICuponService cuponService)
        {
            _cuponService = cuponService;
        }
        [HttpPost("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult AddSpecificCuponForUser([FromBody]AddCuponForUserDTO addCuponForUser, [FromHeader] string LangCode)
        {
            var result=_cuponService.AddSpecificCuponForUser(addCuponForUser, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult AddSpecificCuponForCategory([FromBody]AddCuponForCategoryDTO addCuponForCategoryDTO, [FromHeader]string LangCode)
        {
            var result=_cuponService.AddSpecificCuponForCategory(addCuponForCategoryDTO, LangCode);
            return StatusCode((int)result.StatusCode, result);

        }
        [HttpPost("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult AddSpecificCuponForSubCategory([FromBody]AddCuponForSubCategoryDTO addCuponForSubCategoryDTO, [FromHeader]string LangCode)
        {
            var result=_cuponService.AddSpecificCuponForSubCategory(addCuponForSubCategoryDTO, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult AddSpecificCuponForProduct([FromBody]AddCuponForProductDTO addCuponForProductDTO, [FromHeader]string LangCode)
        {
            var result= _cuponService.AddSpecificCuponForProduct(addCuponForProductDTO, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpDelete("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult RemoveCupon([FromQuery] Guid Id)
        {
            var result=_cuponService.RemoveCupon(Id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult ChangeStatusCupon( [FromBody]UpdateStatusCuponDTO updateStatusCuponDTO,[FromHeader] string LangCode)
        {
            var result = _cuponService.ChangeStatusCupon(updateStatusCuponDTO, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPut("[action]")]
        [Authorize(Policy = "AllRole")]
        public IActionResult UpdateCupon([FromBody] UpdateCuponDTO updateCuponDTO, [FromHeader]string LangCode)
        {
            var result=_cuponService.UpdateCupon(updateCuponDTO, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
     
        public IActionResult CheckedCuponCode([FromQuery] string CuponCode)
        {
            var result=_cuponService.CheckedCuponCode(CuponCode);
  return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        public IActionResult GetCuponDetail([FromQuery]Guid CuponId, [FromHeader]string LangCode)
        {
            var result = _cuponService.GetCuponDetail(CuponId, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCupon([FromQuery]int Page, [FromHeader] string LangCode)
        {
            var result = await _cuponService.GetAllCuponAsync(Page, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
