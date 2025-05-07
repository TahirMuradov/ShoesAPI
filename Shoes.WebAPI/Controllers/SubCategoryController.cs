using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Core.Utilites.Results.Concrete;
using Shoes.Entites.DTOs.SubCategoryDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AllRole")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        [HttpGet("[action]")]
        public IActionResult GetSubCategoryForUpdate(Guid Id)
        {
            var result=_subCategoryService.GetSubCategoryForUpdate(Id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("[action]")]
        public IActionResult AddSubCategory([FromBody] AddSubCategoryDTO subCategoryDTO, [FromHeader] string LangCode)
        {
            var result = _subCategoryService.AddSubCategory(subCategoryDTO, LangCode);

            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateSubCategory([FromBody] UpdateSubCategoryDTO subCategoryDTO, [FromHeader] string LangCode)
        {
            var result = _subCategoryService.UpdateSubCategory(subCategoryDTO, langCode: LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpDelete("[action]")]
        public IActionResult DeleteSubCategory([FromQuery] Guid Id)
        {
            var result = _subCategoryService.DeleteSubCategory(Id);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
       public IActionResult GetSubCategory([FromQuery]Guid Id,[FromHeader]string LangCode)
        {
            var result=_subCategoryService.GetSubCategory(Id, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSubCategoryForTable([FromQuery]int page, [FromHeader] string LangCode)
        {
            var result = await _subCategoryService.GetAllSubCategoryForTableAsync(LangCode,page);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        public  IActionResult GetAllSubCategory( [FromHeader] string LangCode)
        {
            var result = _subCategoryService.GetAllSubCategory(LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
