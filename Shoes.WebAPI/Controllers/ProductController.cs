using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.PictureDTOs;
using Shoes.Entites.DTOs.ProductDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("[action]")]
        public IActionResult AddProduct(AddProductDTO addProductDTO, [FromHeader] string LangCode)
        {
            var result = _productService.AddProduct(addProductDTO,LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateProduct(UpdateProductDTO updateProductDTO, [FromHeader] string LangCode)
        {
            var result = _productService.UpdateProduct(updateProductDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var result=_productService.DeleteProduct(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProductDashboard([FromQuery] int page, [FromHeader] string LangCode)
        {
            var result = await _productService.GetAllProductDashboardAsync(LangCode, page);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProduct([FromBody] GetProductFilterParamsDTO filterParamsDTO ,  [FromHeader] string LangCode)
        {
            var result = await _productService.GetAllProductAsync(filterParamsDTO.subCategoryId, filterParamsDTO. SizeId, LangCode, filterParamsDTO. page, filterParamsDTO.minPrice, filterParamsDTO.maxPrice);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
