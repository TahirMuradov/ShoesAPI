using FluentValidation;
using Shoes.Entites.DTOs.PictureDTOs;
using System.Globalization;

namespace Shoes.Bussines.FluentValidations.PictureDTOValidations
{
    public class AddPictureValidation:AbstractValidator<AddPictureDTO>
    {
        public AddPictureValidation(string LangCode)
        {
            RuleFor(x => x.ProductId)
    .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("ProductIdRequired", new CultureInfo(LangCode)));

            RuleFor(x => x.Pictures)
                   .Must(pictures => pictures != null && pictures.Count > 0)
                .NotEmpty().WithMessage(ValidatorOptions.Global.LanguageManager.GetString("PicturesRequired", new CultureInfo(LangCode)));
             
               

            RuleForEach(x => x.Pictures).ChildRules(picture =>
            {
                picture.RuleFor(x => x.Length)
                    .GreaterThan(0).WithMessage(ValidatorOptions.Global.LanguageManager.GetString("EmptyFileNotAllowed", new CultureInfo(LangCode)));

                picture.RuleFor(x => x.ContentType)
                    .Must(contentType => contentType.Equals("image/jpeg") || contentType.Equals("image/png"))
                    .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("InvalidImageFormat", new CultureInfo(LangCode)));
            });
        }
    }
}
