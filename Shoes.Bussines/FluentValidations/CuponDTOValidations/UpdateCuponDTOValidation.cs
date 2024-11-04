using FluentValidation;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.CuponDTOValidations
{
    public class UpdateCuponDTOValidation:AbstractValidator<UpdateCuponDTO>
    {
        public UpdateCuponDTOValidation(string LangCode)
        {
           
            RuleFor(x => x.CuponId)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("CuponIdrequired", new CultureInfo(LangCode)));

            RuleFor(x => x)
                .Must(dto => dto.CategoryId.HasValue || dto.SubCategoryId.HasValue || dto.UserId.HasValue || dto.ProductId.HasValue)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("RealetedIdrequired", new CultureInfo(LangCode)));

            RuleFor(x => x.DisCountPercent)
                .InclusiveBetween(0, 100)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DisCountPrecentBeetwen", new CultureInfo(LangCode)));
        }
    }
}
