using FluentValidation;
using Shoes.Entites.DTOs.SubCategoryDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.SubCategoryDTOValidations
{
    public class AddSubCategoryDTOValidator:AbstractValidator<AddSubCategoryDTO>
    {
        public AddSubCategoryDTOValidator(string langCode)
        {

            // Validate that CategoryId is not null, empty, or the default value
            RuleFor(dto => dto.CategoryId)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("CategoryIdInvalid", new CultureInfo(langCode)))
                .Must(id => id != Guid.Empty)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("CategoryIdInvalid", new CultureInfo(langCode)));

            // Validate that LangContent contains exactly three entries and the keys are valid language codes
            RuleFor(dto => dto.LangContent)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(langCode)))
                .Must(langContent => langContent != null && langContent.Count == 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(langCode)));

            // Validate each key in LangContent
            RuleForEach(dto => dto.LangContent.Keys)
                .Must(key => new[] { "az", "ru", "en" }.Contains(key))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(langCode)));

            // Validate each value in LangContent
            RuleForEach(dto => dto.LangContent.Values)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(langCode)));

        }
    }
}
