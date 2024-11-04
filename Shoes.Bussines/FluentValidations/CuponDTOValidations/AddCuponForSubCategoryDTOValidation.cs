using FluentValidation;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.CuponDTOValidations
{
    public class AddCuponForSubCategoryDTOValidation:AbstractValidator<AddCuponForSubCategoryDTO>
    {
        public AddCuponForSubCategoryDTOValidation(string LangCode)
        {
            RuleFor(c => c.SubCategoryId)
.NotEmpty()
.WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoryIdrequired", new CultureInfo(LangCode)));

            RuleFor(c => c.DisCountPercent)
                 .InclusiveBetween(0, 100)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DisCountPrecentBeetwen", new CultureInfo(LangCode)));

        }
    }
}
