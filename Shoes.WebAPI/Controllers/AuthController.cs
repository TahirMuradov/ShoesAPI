using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shoes.Entites;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

  
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser()
        {
            User user=new User()
            {
                Email="saas",
               
            };
            var result = await _userManager.CreateAsync(user, "123");

       

            return result.Succeeded? Ok( result):BadRequest(result);
        }
    }
}
