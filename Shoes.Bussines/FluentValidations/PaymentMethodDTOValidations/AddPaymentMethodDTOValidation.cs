using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.PaymentMethodDTOs;
using System.Globalization;
using System.Linq;

namespace Shoes.Bussines.FluentValidations.PaymentMethodDTOValidations
{
    public class AddPaymentMethodDTOValidation:AbstractValidator<AddPaymentMethodDTO>
    {
        public AddPaymentMethodDTOValidation(string LangCode)
        {

            var SupportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
            // Validate that LangContent is not null and contains at least three entries
            RuleFor(dto => dto.Lang)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(LangCode)))
                .Must(langContent => langContent != null && langContent.Count == 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(LangCode)));

            // Validate that each key in LangContent is a valid language code
            RuleForEach(dto => dto.Lang.Keys)
                .Must(key =>SupportedLaunguages.Contains(key))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(LangCode)));

            // Validate that each value in LangContent is not null or empty
            RuleForEach(dto => dto.Lang.Values)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(LangCode)));

        }
    }
}
