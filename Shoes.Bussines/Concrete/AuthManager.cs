using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.AuthStatusMessages;
using Shoes.Bussines.FluentValidations.AuthDTOValidations;
using Shoes.Core.Entities.Concrete;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.EmailHelper.Abstract;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Security.Abstarct;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.Entites;
using Shoes.Entites.DTOs.AuthDTOs;
using System.Net;
using System.Web;

namespace Shoes.Bussines.Concrete
{
    public class AuthManager : IAuthService
    {

        private string[] SupportedLaunguages
        {
            get
            {

                return ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();


            }
        }

        private string DefaultLaunguage
        {
            get
            {
                return ConfigurationHelper.config.GetSection("SupportedLanguage:Default").Get<string>();
            }
        }
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailHelper _emailHelper;
        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<AppRole> roleManager, ITokenService tokenService, IEmailHelper emailHelper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _emailHelper = emailHelper;
            _configuration = configuration;
        }

        public async Task<IResult> AssignRoleToUserAsnyc(AssignRoleDTO assignRoleDTO, string culture)
        {
            if (!SupportedLaunguages.Contains(culture))
                culture = DefaultLaunguage;
            AssignRoleDTOValidator validationRules = new AssignRoleDTOValidator(culture);
            var validationResult = await validationRules.ValidateAsync(assignRoleDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), statusCode: HttpStatusCode.BadRequest);

            User user = await _userManager.FindByIdAsync(assignRoleDTO.UserId.ToString());
            string responseMessage = string.Empty;
            if (user == null)
                return new ErrorResult(AuthStatusMessage.UserNotFound[culture], HttpStatusCode.NotFound);
            else
            {
                AppRole role = await _roleManager.FindByIdAsync(assignRoleDTO.RoleId.ToString());
                if (role == null)
                    return new ErrorResult(AuthStatusMessage.RoleNotFound[culture], HttpStatusCode.NotFound);
                IdentityResult identityResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (!identityResult.Succeeded)
                    return new ErrorResult(messages: identityResult.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);


                return new SuccessResult(HttpStatusCode.OK);
            }
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO, string culture)
        {
            if (!SupportedLaunguages.Contains(culture))
                culture = DefaultLaunguage;
            LoginDTOValidation validationRules = new LoginDTOValidation(culture);
            var ValidationResult = await validationRules.ValidateAsync(loginDTO);
            if (!ValidationResult.IsValid)
                return new ErrorDataResult<Token>(messages: ValidationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
                return new ErrorDataResult<Token>(AuthStatusMessage.UserNotFound[culture], HttpStatusCode.NotFound);


            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            var roles = await _userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                Token token = await _tokenService.CreateAccessTokenAsync(user, roles.ToList());
                var response = await UpdateRefreshTokenAsnyc(refreshToken: token.RefreshToken, user, culture);
                if (response.IsSuccess)
                    return new SuccessDataResult<Token>(response: token, statusCode: HttpStatusCode.OK, message: response.Message);
                else
                    return new ErrorDataResult<Token>(statusCode: HttpStatusCode.BadRequest, message: response.Message);
            }
            else
                return new ErrorDataResult<Token>(statusCode: HttpStatusCode.BadRequest, message: AuthStatusMessage.UserNotFound[culture]);
        }

        public async Task<IResult> LogOutAsync(string userId, string culture)
        {
            if (!SupportedLaunguages.Contains(culture))
                culture = DefaultLaunguage;
            if (string.IsNullOrEmpty(userId)) return new ErrorResult(statusCode: HttpStatusCode.NotFound, message: AuthStatusMessage.UserNotFound[culture]);

            var findUser = await _userManager.FindByIdAsync(userId);
            if (findUser == null)
                return new ErrorResult(statusCode: HttpStatusCode.NotFound, message: AuthStatusMessage.UserNotFound[culture]);


            findUser.RefreshToken = null;
            findUser.RefreshTokenExpiredDate = null;
            var result = await _userManager.UpdateAsync(findUser);
            if (result.Succeeded)
            {
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            else
            {
                string responseMessage = string.Empty;
                foreach (var error in result.Errors)
                {
                    responseMessage += error + ". ";
                };
                return new ErrorDataResult<Token>(statusCode: HttpStatusCode.BadRequest, message: responseMessage);
            }
        }

        public async Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken, string culture)
        {
            if (!SupportedLaunguages.Contains(culture))
                culture = DefaultLaunguage;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user is null) return new ErrorDataResult<Token>(message: AuthStatusMessage.UserNotFound[culture], HttpStatusCode.NotFound);
            var roles = await _userManager.GetRolesAsync(user);

            if (user != null && user?.RefreshTokenExpiredDate > DateTime.UtcNow.AddHours(4))
            {
                Token token = await _tokenService.CreateAccessTokenAsync(user, roles.ToList());
                token.RefreshToken = refreshToken;
                return new SuccessDataResult<Token>(response: token, statusCode: HttpStatusCode.OK);
            }
            else
                return new ErrorDataResult<Token>(statusCode: HttpStatusCode.BadRequest, message: AuthStatusMessage.UserNotFound[culture]);
        }

        public async Task<IResult> RegisterAsync(RegisterDTO registerDTO, string culture)
        {

            if (!SupportedLaunguages.Contains(culture))
                culture = DefaultLaunguage;
            RegisterDTOValidation validationRules = new RegisterDTOValidation(culture);
            var validationResult = await validationRules.ValidateAsync(registerDTO);
            if (!validationResult.IsValid) return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            var checkEmail = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == registerDTO.Email);
            var checkUserName = await _userManager.FindByNameAsync(registerDTO.Username);

            if (checkEmail != null)
                return new ErrorResult(statusCode: HttpStatusCode.BadRequest, message: AuthStatusMessage.EmailAlreadyExists[culture]);

            if (checkUserName != null)
                return new ErrorResult(statusCode: HttpStatusCode.BadRequest, message: AuthStatusMessage.UserNameAlreadyExists[culture]);

            User newUser = new()
            {
                FirstName = registerDTO.Firstname,
                LastName = registerDTO.Lastname,
                Email = registerDTO.Email,
                UserName = registerDTO.Username,
                PhoneNumber = registerDTO.PhoneNumber,
                Adress = registerDTO.Adress,


            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (identityResult.Succeeded)
            {
                if (_userManager.Users.Count() == 1)
                {
                    if (_roleManager.Roles.Count() == 0)
                    {

                        AppRole appRole = new AppRole()
                        {
                            Name = "SuperAdmin"
                        };
                        AppRole appRole1 = new AppRole()
                        {
                            Name = "Admin"
                        };
                        await _roleManager.CreateAsync(appRole);
                        await _roleManager.CreateAsync(appRole1);
                    }
                    await _userManager.AddToRoleAsync(newUser, "SuperAdmin");
                }
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
           
                string confimationLink = $"{ConfigurationHelper.config.GetSection("Domain:Front").Get<string>()}/{culture}/auth/emailconfirmed/{HttpUtility.UrlEncode(newUser.Email)}/{HttpUtility.UrlEncode(token)}";
                var resultEmail = await _emailHelper.SendEmailAsync(newUser.Email, confimationLink, newUser.FirstName + newUser.LastName);
                if (!resultEmail.IsSuccess)
                {
                    await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(newUser.Email));
                    return new ErrorResult(message: AuthStatusMessage.ConfirmationLinkNotSend.GetValueOrDefault(culture), HttpStatusCode.BadRequest);
                }
                return new SuccessResult(message: AuthStatusMessage.RegistrationSuccess[culture], statusCode: HttpStatusCode.Created);
            }
            else
            {
                string responseMessage = string.Empty;
                foreach (var error in identityResult.Errors)
                    responseMessage += $"{error.Description}. ";
                return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> RemoveRoleFromUserAsync(RemoveRoleUserDTO removeRoleUserDTO, string culture)
        {
            if (!SupportedLaunguages.Contains(culture))
                culture = DefaultLaunguage;
            RemoveRoleUserDTOValidation validationRules = new RemoveRoleUserDTOValidation(culture);
            var validationResult = await validationRules.ValidateAsync(removeRoleUserDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);


            User user = await _userManager.FindByIdAsync(removeRoleUserDTO.UserId.ToString());
            string responseMessage = string.Empty;
            if (user == null)
                return new ErrorResult(AuthStatusMessage.UserNotFound[culture], HttpStatusCode.NotFound);
            else
            {
                foreach (var roleid in removeRoleUserDTO.RoleId)
                {
                    AppRole role = await _roleManager.FindByIdAsync(roleid.ToString());
                    if (role == null)
                        return new ErrorResult(AuthStatusMessage.RoleNotFound[culture], HttpStatusCode.NotFound);
                    IdentityResult identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (!identityResult.Succeeded)
                    {
                        foreach (var error in identityResult.Errors)
                            responseMessage += $"{error.Description}. ";
                        return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
                    }
                }


                return new SuccessResult(HttpStatusCode.OK);
            }
        }

        public async Task<IDataResult<string>> UpdateRefreshTokenAsnyc(string refreshToken, User user, string culture)
        {
            if (!SupportedLaunguages.Contains(culture))
                culture = DefaultLaunguage;


            if (user is not null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiredDate = DateTime.UtcNow.AddMonths(1);

                IdentityResult identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                    return new SuccessDataResult<string>(statusCode: HttpStatusCode.OK, response: refreshToken);
                else
                    return new ErrorDataResult<string>(messages: identityResult.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);

            }
            else
                return new ErrorDataResult<string>(AuthStatusMessage.UserNotFound[culture], HttpStatusCode.NotFound);
        }

        public async Task<IDataResult<PaginatedList<GetAllUserDTO>>> GetAllUserAsnyc(int page)
        {
            if (page < 1)
                page = 1;
            
            IQueryable<GetAllUserDTO> query = _userManager.Users.Select(x => new GetAllUserDTO
            {
                Id = x.Id,
                Adress = x.Adress,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName

            });
            var result = await PaginatedList<GetAllUserDTO>.CreateAsync(query, page, 10);
            return new SuccessDataResult<PaginatedList<GetAllUserDTO>>(response: result, HttpStatusCode.OK);

        }


        public async Task<IResult> ChecekdConfirmedEmailTokenAsnyc(string email, string token, string culture)
        {
            var checekedEmail = await _userManager.FindByEmailAsync(email);
            if (checekedEmail is null) return new ErrorResult(message: AuthStatusMessage.UserNotFound.GetValueOrDefault(culture), HttpStatusCode.NotFound);

            if (checekedEmail.EmailConfirmed)
                return new ErrorResult(HttpStatusCode.BadRequest);
            IdentityResult checekedResult = await _userManager.ConfirmEmailAsync(checekedEmail, token);
            if (checekedResult.Succeeded)

                return new SuccessResult(messages: checekedResult.Errors.Select(x => x.Description).ToList(), HttpStatusCode.OK);

            else
                return new ErrorResult(messages: checekedResult.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);


        }

        public async Task<IResult> EditUserProfileAsnyc(UpdateUserDTO updateUserDTO, string culture)
        {

            var checekedUser = await _userManager.FindByIdAsync(updateUserDTO.UserId.ToString());
            if (checekedUser is null) return new ErrorResult(message: AuthStatusMessage.UserNotFound.GetValueOrDefault(culture), HttpStatusCode.NotFound);
            if (!string.IsNullOrEmpty(updateUserDTO.Firstname) && checekedUser.FirstName != updateUserDTO.Firstname)
                checekedUser.FirstName = updateUserDTO.Firstname;
            if (!string.IsNullOrEmpty(updateUserDTO.Username) && checekedUser.UserName != updateUserDTO.Username)
                checekedUser.UserName = updateUserDTO.Username;
            if (!string.IsNullOrEmpty(updateUserDTO.Lastname) && checekedUser.LastName != updateUserDTO.Lastname)
                checekedUser.LastName = updateUserDTO.Lastname;
            if (!string.IsNullOrEmpty(updateUserDTO.Adress) && checekedUser.Adress != updateUserDTO.Adress)
                checekedUser.Adress = updateUserDTO.Adress;
            if (!string.IsNullOrEmpty(updateUserDTO.PhoneNumber) && checekedUser.PhoneNumber != updateUserDTO.PhoneNumber)
                checekedUser.PhoneNumber = updateUserDTO.PhoneNumber;
            if (!string.IsNullOrEmpty(updateUserDTO.CurrentPassword) && !string.IsNullOrEmpty(updateUserDTO.NewPassword))
            {

                IdentityResult changePassword = await _userManager.ChangePasswordAsync(checekedUser, updateUserDTO.CurrentPassword, updateUserDTO.NewPassword);
                if (!changePassword.Succeeded)
                    return new ErrorResult(messages: changePassword.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);
            }
            IdentityResult UpdateUserResult = await _userManager.UpdateAsync(checekedUser);


            return UpdateUserResult.Succeeded ? new SuccessResult(HttpStatusCode.OK) :
                  new ErrorResult(messages: UpdateUserResult.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);

        }

        public async Task<IResult> DeleteUserAsnyc(Guid Id, string culture)
        {
            User ChecekdUSerId = await _userManager.FindByIdAsync(Id.ToString());
            if (ChecekdUSerId == null) return new ErrorResult(message: AuthStatusMessage.UserNotFound[culture], HttpStatusCode.NotFound);
            IdentityResult result = await _userManager.DeleteAsync(ChecekdUSerId);
            if (result.Succeeded)
                return new SuccessResult(HttpStatusCode.OK);
            else
                return new ErrorResult(messages: result.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);

        }

        public async Task<IResult> SendEmailTokenForForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email)) return new ErrorResult(HttpStatusCode.BadRequest);
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = _configuration["Domain:Front"]+ $"/auth/forgotpassword/confirmation/{HttpUtility.UrlEncode(Email)}/{HttpUtility.UrlEncode(token)}";
          var emailResult=await  _emailHelper.SendEmailAsync(user.Email, url, user.FirstName + user.LastName);


            if (emailResult.IsSuccess)
        
                return new SuccessResult(HttpStatusCode.OK);

            return new ErrorResult(HttpStatusCode.BadRequest);
        }

        public async Task<IResult> CheckTokenForForgotPassword(string Email, string token)
        {
            if (string.IsNullOrEmpty(Email)||string.IsNullOrEmpty(token)) return new ErrorResult(HttpStatusCode.BadRequest);
            
            Email=HttpUtility.UrlDecode(Email);
            token=HttpUtility.UrlDecode(token);
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
              return new ErrorResult(HttpStatusCode.NotFound);
                bool tokenResult = await _userManager.VerifyUserTokenAsync(
       user: user,
       tokenProvider: _userManager.Options.Tokens.PasswordResetTokenProvider,
       purpose: UserManager<User>.ResetPasswordTokenPurpose,
       token: token
                      );

            if (tokenResult) return new SuccessResult(HttpStatusCode.OK);
            return new ErrorResult(HttpStatusCode.BadRequest);
        }

        public async Task<IResult> ChangePasswordForTokenForgotPassword(string Email, string token, string NewPassword)
        {
            if (string.IsNullOrEmpty(Email)||string.IsNullOrEmpty(token)||string.IsNullOrEmpty(NewPassword))
           return new ErrorResult(HttpStatusCode.BadRequest);
            Email = HttpUtility.UrlDecode(Email);
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
                return new ErrorResult(HttpStatusCode.NotFound);

            token = HttpUtility.UrlDecode(token);
            NewPassword= HttpUtility.UrlDecode(NewPassword);
            var tokenResult = await _userManager.ResetPasswordAsync(user, token, NewPassword);
            if(tokenResult.Succeeded)
                return new SuccessResult(HttpStatusCode.OK);
            return new ErrorResult(messages:tokenResult.Errors.Select(x=>x.Description).ToList(),HttpStatusCode.BadRequest);

        }
    }
}

