using Microsoft.AspNetCore.Mvc;
using Shoes.Entites.DTOs.PictureDTOs;
using Shoes.Entites.DTOs.ProductDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost("[action]")]
        public IActionResult AddProduct(AddProductDTO addProductDTO, [FromHeader] string LangCode)
        {
            return BadRequest(addProductDTO); ;
        }
    }
}
