using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        [HttpGet("[action]")]
        public IActionResult GetAllData([FromHeader] string LangCode)
        {
            var result = _homeService.GetHomeAllData(LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);


        }
    }
}
