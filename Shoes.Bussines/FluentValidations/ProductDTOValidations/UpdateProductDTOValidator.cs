using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.ProductDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.ProductDTOValidations
{
    public class UpdateProductDTOValidator:AbstractValidator<UpdateProductDTO>
    {
        private string[] SupportedLanguages
        {
            get
            {
                return ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
            }
        }
        public UpdateProductDTOValidator(string LangCode)
        {  // Validate Id
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdRequired", new CultureInfo(LangCode)))
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdRequired", new CultureInfo(LangCode)))
                .NotEqual(Guid.Empty)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("IdInvalid", new CultureInfo(LangCode)));

            // Validate ProductName
            RuleFor(x => x.ProductName)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ProductNameRequired", new CultureInfo(LangCode)))
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ProductNameRequired", new CultureInfo(LangCode)))
                .Must(productName => productName.Keys.All(k => SupportedLanguages.Contains(k)))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(LangCode)));

            // Validate Description
            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DescriptionRequired", new CultureInfo(LangCode)))
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DescriptionRequired", new CultureInfo(LangCode)))
                .Must(description => description.Keys.All(k => SupportedLanguages.Contains(k)))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(LangCode)));

            // Validate SubCategoriesID
            RuleFor(x => x.SubCategoriesID)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoriesIDRequired", new CultureInfo(LangCode)))
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoriesIDRequired", new CultureInfo(LangCode)))
                .Must(subCategories => subCategories.Count > 0)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoriesIDLength", new CultureInfo(LangCode)));

            // Validate CurrentPictureUrls and NewPictures
            RuleFor(x => x)
                .Must(x => !(x.CurrentPictureUrls == null || x.CurrentPictureUrls.Count == 0) || !(x.NewPictures == null || x.NewPictures.Count == 0))
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PictureUrlsOrNewPicturesRequired", new CultureInfo(LangCode)));

            // Validate Sizes
            RuleFor(x => x.Sizes)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SizesRequired", new CultureInfo(LangCode)))
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SizesRequired", new CultureInfo(LangCode)));
            

            // Validate DiscountPrice and Price
            RuleFor(x => x.DiscountPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("DiscountPriceInvalid", new CultureInfo(LangCode)));

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PriceInvalid", new CultureInfo(LangCode)));

            // Validate ProductCode
            RuleFor(x => x.ProductCode)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ProductCodeRequired", new CultureInfo(LangCode)))
                .NotEmpty()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ProductCodeRequired", new CultureInfo(LangCode)));
        }

    }
    }

