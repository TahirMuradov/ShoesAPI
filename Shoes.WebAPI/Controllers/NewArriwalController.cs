using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewArriwalController : ControllerBase
    {
        private readonly INewArriwalService _arriwalService;

        public NewArriwalController(INewArriwalService arriwalService)
        {
            _arriwalService = arriwalService;
        }
        [HttpGet("[action]")]
        public IActionResult GetNewArriwalProduct([FromHeader] string LangCode)
        {
            var result=_arriwalService.GetNewArriwalProducts(LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetNewArriwalCategories([FromHeader] string LangCode)
        {
            var result=_arriwalService.GetNewArriwalCategories(LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result) ;
        }
    }
}
