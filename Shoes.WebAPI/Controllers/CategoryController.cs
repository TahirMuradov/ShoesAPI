using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.CategoryDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result=_categoryService.AddCategory(addCategoryDTO,LangCode);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult DeleteCategory(Guid Id, [FromHeader] string LangCode)
        {
            if (LangCode is null)
                return BadRequest();
            var result= _categoryService.DeleteCategory(Id,LangCode);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }

    }
}
