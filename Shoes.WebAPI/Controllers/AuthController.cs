using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using Shoes.Bussines.Abstarct;
using Shoes.Entites;
using Shoes.Entites.DTOs.AuthDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
     
        [HttpGet("[action]")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> GetAllUser([FromQuery]int page)
        {
            var result=await _authService.GetAllUserAsnyc(page);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO, [FromHeader] string LangCode)
        {
            var result= await _authService.LoginAsync(loginDTO, LangCode);
            return result.IsSuccess? Ok(result):BadRequest(result) ;
        }
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> LogOut([FromQuery] Guid UserId, [FromHeader]string LangCode)
        {
            var result = await _authService.LogOutAsync(UserId.ToString(), LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);   
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> EditUserProfile([FromBody] UpdateUserDTO updateUserDTO, [FromHeader] string LangCode)
        {
            var result=await _authService.EditUserProfileAsnyc(updateUserDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> DeleteUser([FromQuery]Guid Id, [FromHeader] string LangCode)
        {
            var result=await _authService.DeleteUserAsnyc(Id, LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
        [HttpDelete("[action]")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] RemoveRoleUserDTO removeRoleUserDTO, [FromHeader] string LangCode)
        {
            var result=await _authService.RemoveRoleFromUserAsync(removeRoleUserDTO, LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registerDTO, [FromHeader] string LangCode)
        {
            
            var result=await _authService.RegisterAsync(registerDTO, LangCode);
            return result.IsSuccess?Ok(result):BadRequest(result) ;
        }
        [HttpPatch("[action]")]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> AssignRoleToUser([FromBody]AssignRoleDTO assignRoleDTO, [FromHeader] string LangCode)
        {
            var result=await _authService.AssignRoleToUserAsnyc(assignRoleDTO, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> ChecekdConfirmedEmailToken([FromBody] ConfirmedEmailDTO confirmedEmailDTO, [FromHeader] string LangCode)
        {
            if (string.IsNullOrEmpty(confirmedEmailDTO.Email) || string.IsNullOrEmpty(confirmedEmailDTO.token) || string.IsNullOrEmpty(LangCode)) return BadRequest();
            var result = await _authService.ChecekdConfirmedEmailTokenAsnyc(confirmedEmailDTO.Email,confirmedEmailDTO.token, LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
