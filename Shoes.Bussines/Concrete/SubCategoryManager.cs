using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.SubCategoryDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.SubCategoryDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class SubCategoryManager : ISubCategoryService
    {
        private string[] SupportedLaunguages
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
        private readonly ISubCategoryDAL _subCategoryDAL;

        public SubCategoryManager(ISubCategoryDAL subCategoryDAL)
        {
            _subCategoryDAL = subCategoryDAL;
        }


        public IResult AddSubCategory(AddSubCategoryDTO addCategory, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            AddSubCategoryDTOValidator validationRules = new(langCode: LangCode);
            var ValidationResult = validationRules.Validate(addCategory);
            if (!ValidationResult.IsValid)
            {
                List<string> errors = ValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(messages: errors, HttpStatusCode.BadRequest);

            }
            return _subCategoryDAL.AddSubCategory(addCategory);
        }
     

        public IResult DeleteSubCategory(Guid Id)
        {
            if (Id == default)
                return new ErrorResult(HttpStatusCode.BadRequest);
            return _subCategoryDAL.DeleteSubCategory(Id);

        }

        public async Task<IDataResult<PaginatedList<GetSubCategoryDTO>>> GetAllSubCategoryForTableAsync(string LangCode, int page = 1)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            if (page <= 0)
                page = 1;
            return await _subCategoryDAL.GetAllSubCategoryForTableAsync(LangCode: LangCode,page: page);

        }

        public IDataResult<GetSubCategoryDTO> GetSubCategory(Guid Id, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            if (Id==default)
                return new ErrorDataResult<GetSubCategoryDTO>(HttpStatusCode.BadRequest);
            return _subCategoryDAL.GetSubCategory(Id,LangCode);
        }

        public IResult UpdateSubCategory(UpdateSubCategoryDTO updateCategory, string langCode)
        {
            if (string.IsNullOrEmpty(langCode) || !SupportedLaunguages.Contains(langCode))
                langCode = DefaultLaunguage;
            UpdateSubCategoryDTOValidator validationRules = new(langCode);
            var validationResult=validationRules.Validate(updateCategory);
            if (!validationResult.IsValid)
            {
                List<string> messages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
           return new ErrorResult(messages:messages,statusCode: HttpStatusCode.BadRequest);
            }
            return _subCategoryDAL.UpdateSubCategory(updateCategory);

        }

        public IDataResult<GetSubCategoryForUpdateDTO> GetSubCategoryForUpdate(Guid Id)
        {
            if (Id == default)
                return new ErrorDataResult<GetSubCategoryForUpdateDTO>(HttpStatusCode.BadRequest);
            return _subCategoryDAL.GetSubCategoryForUpdate(Id);
        }

        public IDataResult<IQueryable<GetSubCategoryDTO>> GetAllSubCategory(string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            return _subCategoryDAL.GetAllSubCategory(LangCode);
        }
    }
}
