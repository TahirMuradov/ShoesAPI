using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.ShippingMethodDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.ShippingMethodDTOValidations
{
    public class UpdateShippingMethodDTOValidation:AbstractValidator<UpdateShippingMethodDTO>
    {
        public UpdateShippingMethodDTOValidation(string LangCode)
        {
            var SupportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();

            RuleFor(dto => dto.Lang)
                 .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangDictionaryIsRequired", new CultureInfo(LangCode)));

            RuleForEach(dto => dto.Lang)
                .Must(pair => !string.IsNullOrEmpty(pair.Key) && !string.IsNullOrEmpty(pair.Value))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangKeyAndValueRequired", new CultureInfo(LangCode)));

            RuleForEach(dto => dto.Lang.Keys)
                .Must(key => SupportedLaunguages.Contains(key))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangKey", new CultureInfo(LangCode)));


        }
    }
}
