using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.ProductDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.ProductDTOValidations
{
    public class AddProductDTOValidatior : AbstractValidator<AddProductDTO>
    {
        protected string[] SupportedLanguages
        {
            get
            {
                return ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
            }
        }
        public AddProductDTOValidatior(string LangCode)
        {
            RuleForEach(x => x.ProductName)
               .Must(keyValuePair => SupportedLanguages.Contains(keyValuePair.Key))
               .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(LangCode)));

            // Validate Description dictionary
            RuleForEach(x => x.Description)
                .Must(keyValuePair => SupportedLanguages.Contains(keyValuePair.Key))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(LangCode)));

            // Ensure the number of supported languages matches the number of dictionary entries
            RuleFor(x => x.ProductName)
                .Must(productName => productName.Count == SupportedLanguages.Length)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(LangCode)));

            RuleFor(x => x.Description)
                .Must(description =>description.Count == SupportedLanguages.Length)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(LangCode)));

            // Validate Sizes dictionary
            RuleForEach(x => x.Sizes.Values)
                .GreaterThan(0)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangContentTooShort", new CultureInfo(LangCode)));

            // Validate DiscountPrice and Price
            RuleFor(x => x.DiscountPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DisCountChecked", new CultureInfo(LangCode)));

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PriceChecked", new CultureInfo(LangCode)));



            // Validate ProductCode
            RuleFor(x => x.ProductCode)
                .NotNull()
               .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ProductCodeİsRequired", new CultureInfo(LangCode)));

            // Validate SubCategories
            RuleFor(x => x.SubCategories)
                .NotNull()
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoryİsRequired", new CultureInfo(LangCode)));

            //// Validate Pictures
            //RuleFor(x => x.Pictures)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PictureİsRequired", new CultureInfo(LangCode)));
        }
    }
}
