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
        public IActionResult AddSpecificCuponForUser([FromBody]AddCuponForUserDTO addCuponForUser, [FromHeader] string LangCode)
        {
            var result=_cuponService.AddSpecificCuponForUser(addCuponForUser, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("[action]")]
        public IActionResult AddSpecificCuponForCategory([FromBody]AddCuponForCategoryDTO addCuponForCategoryDTO, [FromHeader]string LangCode)
        {
            var result=_cuponService.AddSpecificCuponForCategory(addCuponForCategoryDTO, LangCode);
            return result.IsSuccess? Ok(result) : BadRequest(result) ;

        }
        [HttpPost("[action]")]
        public IActionResult AddSpecificCuponForSubCategory(AddCuponForSubCategoryDTO addCuponForSubCategoryDTO, string LangCode)
        {
            var result=_cuponService.AddSpecificCuponForSubCategory(addCuponForSubCategoryDTO, LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
        [HttpPost("[action]")]
        public IActionResult AddSpecificCuponForProduct(AddCuponForProductDTO addCuponForProductDTO, string LangCode)
        {
            var result= _cuponService.AddSpecificCuponForProduct(addCuponForProductDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult RemoveCupon([FromQuery] Guid Id)
        {
            var result=_cuponService.RemoveCupon(Id);
            return result.IsSuccess?Ok(result):BadRequest(result) ;
        }

        [HttpPut("[action]")]
        public IActionResult ChangeStatusCupon( [FromBody]UpdateStatusCuponDTO updateStatusCuponDTO,[FromHeader] string LangCode)
        {
            var result = _cuponService.ChangeStatusCupon(updateStatusCuponDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateCupon([FromBody] UpdateCuponDTO updateCuponDTO, [FromHeader]string LangCode)
        {
            var result=_cuponService.UpdateCupon(updateCuponDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
      public IActionResult CheckedCuponCode([FromQuery] string CuponCode)
        {
            var result=_cuponService.CheckedCuponCode(CuponCode);
            return result.IsSuccess ?Ok(result):BadRequest(result);
        }
    }
}
