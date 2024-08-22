﻿using FluentValidation;
using Shoes.Entites.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Bussines.FluentValidations.SubCategoryDTOValidations
{
    public class UpdateSubCategoryDTOValidator:AbstractValidator<UpdateSubCategoryDTO>
    {
        public UpdateSubCategoryDTOValidator(string langCode)
        {
            RuleFor(dto => dto.Id)
               .NotEmpty()
               .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoryIdInvalid", new CultureInfo(langCode)))
               .Must(id => id != Guid.Empty)
               .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("SubCategoryIdInvalid", new CultureInfo(langCode)));

            // Validate that LangContent contains exactly three entries and the keys are valid language codes
            RuleFor(dto => dto.LangContent)
                .NotNull()
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangSubCategoryContentTooShort", new CultureInfo(langCode)))
                .Must(langContent => langContent != null && langContent.Count <= 3 &&langContent.Count>0)
                .WithMessage(ValidatorOptions.Global.LanguageManager.GetString("LangSubCategoryContentTooShort", new CultureInfo(langCode)));

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
