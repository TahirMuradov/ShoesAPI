using FluentValidation;
using Shoes.Entites.DTOs.SizeDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.SizeDTOValidations
{
    public class AddSizeDTOValidation:AbstractValidator<AddSizeDTO>
    {
        public AddSizeDTOValidation(string langCode)
        {
            RuleFor(x => x.Size)
                .NotEmpty()
                .Must(size => size !<=0).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SizeNumberIsRequiredd", new CultureInfo(langCode)));
            



        }
    }
}
