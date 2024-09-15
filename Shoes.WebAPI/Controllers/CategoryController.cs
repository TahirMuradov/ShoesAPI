using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.CategoryDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class CategoryController : ControllerBase

    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        public IActionResult AddCategory([FromBody] AddCategoryDTO addCategoryDTO, [FromHeader] string LangCode)
        {
            var result = _categoryService.AddCategory(addCategoryDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult DeleteCategory([FromQuery] Guid Id, [FromHeader] string LangCode)
        {
          
            var result = _categoryService.DeleteCategory(Id, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllCategory([FromHeader] string LangCode)
        {
            var result= _categoryService.GetAllCategory(LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCategoryForTable([FromHeader] string langCode, [FromQuery] int page = 1)
        {
            var result=await _categoryService.GetAllCategoryAsync(langCode, page);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetCategory([FromQuery] Guid Id, [FromHeader] string langCode) 
        { var result=_categoryService.GetCategory(Id, langCode);
            return result.IsSuccess? Ok(result):BadRequest(result); 
        }
        [HttpGet("[action]")]
        public IActionResult GetCategoryForUpdate([FromQuery] Guid Id)
        {
            var result=_categoryService.GetCategoryForUpdate(Id);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateCategory([FromBody]UpdateCategoryDTO updateCategoryDTO ,[FromHeader]string LangCode)
        {
            var result=_categoryService.UpdateCategory(updateCategoryDTO, LangCode);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
    }
}
