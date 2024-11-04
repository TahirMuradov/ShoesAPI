using FluentValidation;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.CuponDTOValidations
{
    public class AddCuponForProductDTOValidation:AbstractValidator<AddCuponForProductDTO>
    {
        public AddCuponForProductDTOValidation(string LangCode)
        {
            RuleFor(c => c.ProductId)
    .NotEmpty()
    .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ProductIdrequired", new CultureInfo(LangCode)));

            RuleFor(c => c.DisCountPercent)
          .InclusiveBetween(0, 100)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DisCountPrecentBeetwen", new CultureInfo(LangCode)));

        }
    }
}
