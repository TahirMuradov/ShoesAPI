﻿using Shoes.Core.Entities.Concrete;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites;
using Shoes.Entites.DTOs.AuthDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IAuthService
    {
        Task<IDataResult<PaginatedList< GetAllUserDTO>>> GetAllUserAsnyc(int page);
        Task<IResult> ChecekdConfirmedEmailTokenAsnyc(string email, string token,string culture);
        Task<IResult> EditUserProfileAsnyc(UpdateUserDTO updateUserDTO,string culture);
        Task<IResult> DeleteUserAsnyc(Guid Id,string culture);
        Task<IResult> RegisterAsync(RegisterDTO registerDTO, string culture);
        Task<IResult> AssignRoleToUserAsnyc(AssignRoleDTO assignRoleDTO, string culture);
        Task<IResult> SendEmailTokenForForgotPassword(string Email);
        Task<IResult> CheckTokenForForgotPassword(string Email, string token);
        Task<IResult> ChangePasswordForTokenForgotPassword(string Email, string token, string NewPassword);
       IDataResult<IQueryable<GetAllUserForSelectDTO>> GetAllUserForSelect();
        Task<IDataResult<string>> UpdateRefreshTokenAsnyc(string refreshToken, User user, string culture);
        Task<IResult> RemoveRoleFromUserAsync(RemoveRoleUserDTO removeRoleUserDTO,string culture);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO, string culture);
        Task<IResult> LogOutAsync(string userId, string culture);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken, string culture);
    }
}
