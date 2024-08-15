using FluentValidation;
using Shoes.Entites.DTOs.CategoryDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.CategoryDTOValidations
{
    public class CategoryUpdateDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public CategoryUpdateDTOValidator(string langCode)
        {
           
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdIsRequired", new CultureInfo(langCode)))
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidGuid", new CultureInfo(langCode)));

            RuleFor(dto => dto.Lang)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangDictionaryIsRequired", new CultureInfo(langCode)));

            RuleForEach(dto => dto.Lang)
                .Must(pair => !string.IsNullOrEmpty(pair.Key) && !string.IsNullOrEmpty(pair.Value))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangKeyAndValueRequired", new CultureInfo(langCode)));

            RuleForEach(dto => dto.Lang.Keys)
                .Must(key => new[] { "az", "ru", "en" }.Contains(key))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangKey", new CultureInfo(langCode)));
        }
    }
}

