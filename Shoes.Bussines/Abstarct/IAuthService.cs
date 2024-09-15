using Shoes.Core.Entities.Concrete;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.AuthDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IAuthService
    {
        Task<IDataResult<PaginatedList< GetAllUserDTO>>> GetAllUser(int page);
        Task<IResult> ConfirmedEmail(string email, string token);
        Task<IResult> EditUserProfile(UpdateUserDTO updateUserDTO,string culture);
        Task<IResult> DeleteUser(Guid Id,string culture);
        Task<IResult> RegisterAsync(RegisterDTO registerDTO, string culture);
        Task<IResult> AssignRoleToUserAsnyc(AssignRoleDTO assignRoleDTO, string culture);
        Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser user, string culture);
        Task<IResult> RemoveRoleFromUserAsync(RemoveRoleUserDTO removeRoleUserDTO,string culture);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO, string culture);
        Task<IResult> LogOutAsync(string userId, string culture);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken, string culture);
    }
}
