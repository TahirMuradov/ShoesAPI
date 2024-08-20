using FluentValidation;
using Shoes.Entites.DTOs.SizeDTOs;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Shoes.Bussines.FluentValidations.SizeDTOValidations
{
    public class SizeUpdateDTOValidation:AbstractValidator<UpdateSizeDTO>
    {
        public SizeUpdateDTOValidation(string langCode)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdIsRequired", new CultureInfo(langCode)));

            // Rule for NewSizeNumber validation
            RuleFor(x => x.NewSizeNumber)
                .GreaterThan(0).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("NewSizeNumberIsRequired", new CultureInfo(langCode)));

        }
    }
}
