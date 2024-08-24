using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.CategoryDTOs;
using System.Globalization;


namespace Shoes.Bussines.FluentValidations.CategoryDTOValidations
{
    public class CategoryAddDTOValidator:AbstractValidator<AddCategoryDTO>
    {
  
        public CategoryAddDTOValidator(string langCode)
        {
           
            
            var SupportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
          
         

            // Validate that LangContent is not null and contains at least three entries
            RuleFor(dto => dto.LangContent)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(langCode)))
                .Must(langContent => langContent != null && langContent.Count == 3)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(langCode)));

            // Validate that each key in LangContent is a valid language code
                     RuleFor(dto => dto.LangContent.Keys)
                .Must(keys => keys.All(key => (SupportedLaunguages).Contains(key)))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(langCode)));


            // Validate that each value in LangContent is not null or empty
            RuleForEach(dto => dto.LangContent.Values)
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ContentEmpty", new CultureInfo(langCode)));
        }
    }
}
