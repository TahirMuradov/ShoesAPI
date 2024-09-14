using FluentValidation;
using Shoes.Entites.DTOs.AuthDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.AuthDTOValidations
{
    public class LoginDTOValidation : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation(string LangCode)
        {
            // Email validation: not null, not empty, and must be a valid email format
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("EmailRequired", new CultureInfo(LangCode)))
                .EmailAddress().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("EmailInvalid", new CultureInfo(LangCode)));

            // Password validation: not null or empty
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PasswordRequired", new CultureInfo(LangCode)));
        }
    }
}
