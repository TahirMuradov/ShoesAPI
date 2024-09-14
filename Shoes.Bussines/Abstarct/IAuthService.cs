using Shoes.Core.Entities.Concrete;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.AuthDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO registerDTO, string culture);
        Task<IResult> AssignRoleToUserAsnyc(AssignRoleDTO assignRoleDTO, string culture);
        Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser user, string culture);
        Task<IResult> RemoveRoleFromUserAsync(RemoveRoleUserDTO removeRoleUserDTO,string culture);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO, string culture);
        Task<IResult> LogOutAsync(string userId, string culture);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken, string culture);
    }
}
