using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.PaymentMethodDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.PaymentMethodDTOValidations
{
    public class UpdatePaymentMethodDTOValidation:AbstractValidator<UpdatePaymentMethodDTO>
    {
        public UpdatePaymentMethodDTOValidation(string langCode)
        {

            var SupportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
            RuleFor(dto => dto.Id)
            .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdIsRequired", new CultureInfo(langCode)))
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidGuid", new CultureInfo(langCode)));

            RuleFor(dto => dto.Lang)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangDictionaryIsRequired", new CultureInfo(langCode)));

            RuleForEach(dto => dto.Lang)
                .Must(pair => !string.IsNullOrEmpty(pair.Key) && !string.IsNullOrEmpty(pair.Value))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangKeyAndValueRequired", new CultureInfo(langCode)));

            RuleForEach(dto => dto.Lang.Keys)
                .Must(key => SupportedLaunguages.Contains(key))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangKey", new CultureInfo(langCode)));
        }
    }
}
