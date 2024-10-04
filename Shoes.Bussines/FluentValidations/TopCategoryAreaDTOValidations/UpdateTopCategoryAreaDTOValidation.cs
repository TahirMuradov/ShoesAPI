using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.TopCategoryAreaDTOValidations
{
    public class UpdateTopCategoryAreaDTOValidation :AbstractValidator<UpdateTopCategoryAreaDTO>
    {
        public UpdateTopCategoryAreaDTOValidation(string culture)
        {
            string[] SupportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
            RuleFor(x => x.Id)
               .NotNull()
               .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdRequired", new CultureInfo(culture)))
               .NotEmpty()
               .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdRequired", new CultureInfo(culture)))
               .NotEqual(Guid.Empty)
               .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdInvalid", new CultureInfo(culture)));
            // Validate that DescriptionContent is not null and contains at least three entries
            RuleFor(dto => dto.Description)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)))
                .Must(langContent => langContent != null && langContent.Count == 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(culture)));

            // Validate that each key in DescriptionContent is a valid language code
            RuleFor(dto => dto.Description.Keys)
       .Must(keys => keys.All(key => (SupportedLaunguages).Contains(key)))
       .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(culture)));


            // Validate that each value in DescriptionContent is not null or empty
            RuleForEach(dto => dto.Description.Values)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)));
            // Validate that TitleContent is not null and contains at least three entries
            RuleFor(dto => dto.Title)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)))
                .Must(langContent => langContent != null && langContent.Count == 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(culture)));

            // Validate that each key in TitleContent is a valid language code
            RuleFor(dto => dto.Title.Keys)
       .Must(keys => keys.All(key => (SupportedLaunguages).Contains(key)))
       .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(culture)));


            // Validate that each value in TitleContent is not null or empty
            RuleForEach(dto => dto.Title.Values)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(culture)));

            //// Validate Pictures
            RuleFor(x => x.NewImage)
                .NotNull()
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PictureİsRequired", new CultureInfo(culture)));
            RuleFor(x => x)
            .Must(dto => !((dto.CategoryId == null || dto.CategoryId == Guid.Empty) &&
            (dto.SubCategoryId == null || dto.SubCategoryId == Guid.Empty)))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoryAndCategoryIdIsrequired", new CultureInfo(culture)));
        }
    }
}
