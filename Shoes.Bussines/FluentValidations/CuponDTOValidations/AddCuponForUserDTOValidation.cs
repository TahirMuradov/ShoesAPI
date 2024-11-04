using FluentValidation;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.CuponDTOValidations
{
    public class AddCuponForUserDTOValidation:AbstractValidator<AddCuponForUserDTO>
    {
        public AddCuponForUserDTOValidation(string LangCode)
        {
            RuleFor(c => c.UserId)
.NotEmpty()
.WithMessage(ValidatorOptions.Global.LanguageManager.GetString("UserIdrequired", new CultureInfo(LangCode)));

            RuleFor(c => c.DisCountPercent)
               .InclusiveBetween(0, 100)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DisCountPrecentBeetwen", new CultureInfo(LangCode)));

        }
    }
}
