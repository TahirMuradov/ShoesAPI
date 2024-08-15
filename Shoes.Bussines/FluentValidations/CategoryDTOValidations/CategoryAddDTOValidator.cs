using FluentValidation;
using Shoes.Entites.DTOs.CategoryDTOs;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Shoes.Bussines.FluentValidations.CategoryDTOValidations
{
    public class CategoryAddDTOValidator:AbstractValidator<AddCategoryDTO>
    {
        public CategoryAddDTOValidator(string langCode)
        {
      
            RuleFor(dto => dto.LangCode)
                .Must((dto, langCodes) => langCodes != null && dto.Content != null && langCodes.Count == dto.Content.Count)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeContentLengthMismatch", new CultureInfo(langCode)));

            RuleFor(dto => dto.LangCode)
            .Must(langCodes => langCodes != null && langCodes.Count >= 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangCodeLengthTooShort", new CultureInfo(langCode)));

            RuleFor(dto => dto.Content)
            .Must(content => content != null && content.Count >= 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentLengthTooShort", new CultureInfo(langCode)));

           
            RuleForEach(dto => dto.LangCode)
                .Must(langCode => new[] { "az", "ru", "en" }.Contains(langCode))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(langCode)));
        }
    }
}
