using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
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
        [Authorize(Policy = "AllRole")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromForm]AddProductDTO addProductDTO, [FromHeader] string LangCode)
        {
            var result =await _productService.AddProductAsync(addProductDTO,LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct([FromForm]UpdateProductDTO updateProductDTO, [FromHeader] string LangCode)
        {
            var result = await _productService.UpdateProductAsync(updateProductDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [Authorize(Policy = "AllRole")]
        [HttpDelete("[action]")]
        public IActionResult DeleteProduct([FromQuery]Guid id)
        {
            var result=_productService.DeleteProduct(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProductDashboard([FromQuery] int page, [FromHeader] string LangCode)
        {
            var result = await _productService.GetAllProductDashboardAsync(LangCode, page);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetProductFilterParamsDTO filterParamsDTO ,[FromHeader] string LangCode)
        {
            var result = await _productService.GetAllProductAsync( filterParamsDTO.subCategoryId,filterParamsDTO.CategoryId,filterParamsDTO. SizeId, LangCode, filterParamsDTO. page, filterParamsDTO.minPrice, filterParamsDTO.maxPrice);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpGet("[action]")]
        public IActionResult GetProductDetail([FromQuery] Guid Id, [FromHeader] string LangCode)
        {
            var result = _productService.GetProductDetail(Id, LangCode);
            return result.IsSuccess ? Ok(result) :BadRequest(result);
        }
        [Authorize(Policy = "AllRole")]
        [HttpGet("[action]")]
        public IActionResult GetProductDetailDashboard([FromQuery] Guid id)
        {

            var result=_productService.GetProductDetailDashboard(id);
            return result.IsSuccess?Ok(result):BadRequest(result) ;
        }
    
    }
}
