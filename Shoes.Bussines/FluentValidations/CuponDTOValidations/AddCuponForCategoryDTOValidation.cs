using FluentValidation;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.CuponDTOValidations
{
    public class AddCuponForCategoryDTOValidation:AbstractValidator<AddCuponForCategoryDTO>
    {
        public AddCuponForCategoryDTOValidation(string LangCode)
        {
            RuleFor(c => c.CategoryId)
       .NotEmpty()
       .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("CategoryIdrequired", new CultureInfo(LangCode)));

            RuleFor(c => c.DisCountPercent)
                  .InclusiveBetween(0, 100)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DisCountPrecentBeetwen", new CultureInfo(LangCode)));
        }
    }
}
