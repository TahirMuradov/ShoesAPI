using FluentValidation;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.CuponDTOValidations
{
    public class UpdateStatusCuponDTOValidation:AbstractValidator<UpdateStatusCuponDTO>
    {
        public UpdateStatusCuponDTOValidation(string LangCode)
        {

            RuleFor(x => x.CuponId)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("CuponIdrequired", new CultureInfo(LangCode)));

            RuleFor(x => x)
                .Must(dto => dto.CategoryId.HasValue || dto.SubCategoryId.HasValue || dto.UserId.HasValue || dto.ProductId.HasValue)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("RealetedIdrequired", new CultureInfo(LangCode)));

        }
    }
}
