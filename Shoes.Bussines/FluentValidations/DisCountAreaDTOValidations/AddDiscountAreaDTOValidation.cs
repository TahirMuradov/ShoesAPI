using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.DisCountAreaDTOValidations
{
    public class AddDiscountAreaDTOValidation:AbstractValidator<AddDisCountAreaDTO>
    {
        public AddDiscountAreaDTOValidation(string culture)
        {
         string[] SupportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();

            // Validate that DescriptionContent is not null and contains at least three entries
            RuleFor(dto => dto.DescriptionContent)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)))
                .Must(langContent => langContent != null && langContent.Count == 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(culture)));

            // Validate that each key in DescriptionContent is a valid language code
            RuleFor(dto => dto.DescriptionContent.Keys)
       .Must(keys => keys.All(key => (SupportedLaunguages).Contains(key)))
       .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(culture)));


            // Validate that each value in DescriptionContent is not null or empty
            RuleForEach(dto => dto.DescriptionContent.Values)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)));
            // Validate that TitleContent is not null and contains at least three entries
            RuleFor(dto => dto.TitleContent)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)))
                .Must(langContent => langContent != null && langContent.Count == 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(culture)));

            // Validate that each key in TitleContent is a valid language code
            RuleFor(dto => dto.TitleContent.Keys)
       .Must(keys => keys.All(key => (SupportedLaunguages).Contains(key)))
       .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(culture)));


            // Validate that each value in TitleContent is not null or empty
            RuleForEach(dto => dto.TitleContent.Values)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)));

        }
    }
}
