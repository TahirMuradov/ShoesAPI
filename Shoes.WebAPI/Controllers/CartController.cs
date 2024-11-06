using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.CartDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("[action]")]
        public IActionResult CheckCartData([FromBody] GetCartDataDTO getCartDataDTO)
        {
            var result=_cartService.CheckCartData(getCartDataDTO);

            return result.IsSuccess? Ok(result):BadRequest(result);
        }
    }
}
