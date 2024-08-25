using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;
using Shoes.Entites.DTOs.ShippingMethodDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.ShippingMethodDTOValidations
{
    public class UpdateShippingMethodDTOValidation:AbstractValidator<UpdateShippingMethodDTO>
    {
        public UpdateShippingMethodDTOValidation(string LangCode)
        {
            var SupportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
           
            // Validate that each key in LangContent is a valid language code
            RuleFor(dto => dto.Lang.Keys)
       .Must(keys =>keys.All(key =>(key is null && (SupportedLaunguages).Contains(null))) && keys.All(key => (SupportedLaunguages).Contains(key)))
       .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidLangCode", new CultureInfo(LangCode)));


          
        }
    }
}
