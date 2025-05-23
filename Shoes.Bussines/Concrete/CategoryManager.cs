﻿using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.CategoryDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.CategoryDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private  string[] SupportedLaunguages
        {
            get
            {

                return ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();


            }
        }

        private string DefaultLaunguage
        {
            get
            {
                return ConfigurationHelper.config.GetSection("SupportedLanguage:Default").Get<string>();
            }
        }
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;

        }

        public IResult AddCategory(AddCategoryDTO categoryDTO, string langCode)
        {
            if (string.IsNullOrEmpty(langCode) || !SupportedLaunguages.Contains(langCode))
                langCode = DefaultLaunguage;
           
            CategoryAddDTOValidator validationRules = new CategoryAddDTOValidator(langCode);
            var result = validationRules.Validate(categoryDTO);
            if (!result.IsValid)
                return new ErrorResult(messages: result.Errors.Select(x => x.ErrorMessage).ToList(), statusCode: HttpStatusCode.BadRequest);
            return _categoryDAL.AddCategory(categoryDTO);
        }



        public IResult DeleteCategory(Guid Id, string langCode)
        {
            if (string.IsNullOrEmpty(langCode) || !SupportedLaunguages.Contains(langCode))
                langCode = DefaultLaunguage;
            if (Id == default)
                return new ErrorResult(statusCode: HttpStatusCode.BadRequest);
            return _categoryDAL.DeleteCategory(Id);
        }



        public async Task<IDataResult<PaginatedList<GetCategoryDTO>>> GetAllCategoryAsync(string LangCode, int page = 1)

        {
          
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
       

            if (page < 1)
                page = 1;
            return await _categoryDAL.GetAllCategoryAsync(LangCode, page);
        }

        public IDataResult<GetCategoryDTO> GetCategory(Guid Id, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            if (Id == default(Guid) || string.IsNullOrEmpty(LangCode))
                return new ErrorDataResult<GetCategoryDTO>(statusCode: HttpStatusCode.BadRequest);
            return _categoryDAL.GetCategory(Id, LangCode);
        }

        public IResult UpdateCategory(UpdateCategoryDTO updateCategory, string LangCode)
        {
            


            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            CategoryUpdateDTOValidator validationRules = new CategoryUpdateDTOValidator(LangCode);
            var result = validationRules.Validate(updateCategory);
            if (!result.IsValid)
                return new ErrorResult(messages: result.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

            return _categoryDAL.UpdateCategory(updateCategory);
        }

        public IDataResult<GETCategoryForUpdateDTO> GetCategoryForUpdate(Guid Id)
        {
            if (Id == default)
                return new ErrorDataResult<GETCategoryForUpdateDTO>(HttpStatusCode.NotFound);
            return _categoryDAL.GetCategoryForUpdate(Id);
        }

        public IDataResult<IQueryable<GetCategoryForUIDTO>> GetAllCategory(string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode)||!SupportedLaunguages.Contains(LangCode))
                LangCode= DefaultLaunguage;
        return _categoryDAL.GetAllCategory(LangCode);
        }

        public IDataResult<IQueryable<GetAllCategoryForSelectDTO>> GetAllCategoryForSelect(string LangCode)
        {
            if (!SupportedLaunguages.Contains(LangCode) || string.IsNullOrEmpty(LangCode))
                LangCode = DefaultLaunguage;
            return _categoryDAL.GetAllCategoryForSelect(LangCode);
        }
    }
}
