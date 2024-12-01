using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.CuponDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class CuponManager : ICuponService
    {
        private readonly ICuponDAL _cuponDAL;
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
        public CuponManager(ICuponDAL cuponDAL)
        {
            _cuponDAL = cuponDAL;
        }

        public IDataResult<string> AddSpecificCuponForCategory(AddCuponForCategoryDTO addCuponForCategoryDTO, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            AddCuponForCategoryDTOValidation validationRules = new(LangCode);
            var validationResult=validationRules.Validate(addCuponForCategoryDTO);
            if (!validationResult.IsValid)
                return new ErrorDataResult<string>(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return _cuponDAL.AddSpecificCuponForCategory(addCuponForCategoryDTO);

        }

        public IDataResult<string> AddSpecificCuponForProduct(AddCuponForProductDTO addCuponForProductDTO, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            AddCuponForProductDTOValidation validationRules = new(LangCode);
            var validationResult = validationRules.Validate(addCuponForProductDTO);
            if (!validationResult.IsValid)
                return new ErrorDataResult<string>(messages:validationResult.Errors.Select(x=>x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return _cuponDAL.AddSpecificCuponForProduct(addCuponForProductDTO);
        }

        public IDataResult<string> AddSpecificCuponForSubCategory(AddCuponForSubCategoryDTO addCuponForSubCategoryDTO, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            AddCuponForSubCategoryDTOValidation validationRules = new(LangCode);
            var validationResult = validationRules.Validate(addCuponForSubCategoryDTO);
            if (!validationResult.IsValid)
                return new ErrorDataResult<string>(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return _cuponDAL.AddSpecificCuponForSubCategory(addCuponForSubCategoryDTO);
        }

        public IDataResult<string> AddSpecificCuponForUser(AddCuponForUserDTO addedCuponForUserDTO, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            AddCuponForUserDTOValidation validationRules = new(LangCode);
            var validationResult = validationRules.Validate(addedCuponForUserDTO);
            if (!validationResult.IsValid)
                return new ErrorDataResult<string>(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return  _cuponDAL.AddSpecificCuponForUser(addedCuponForUserDTO);
        }

        public IResult RemoveCupon(Guid Id)
        {
          if(Id ==Guid.Empty)
                return new ErrorResult(HttpStatusCode.BadRequest);
          return _cuponDAL.RemoveCupon(Id);
        }

        public IResult ChangeStatusCupon(UpdateStatusCuponDTO updateStatusCuponDTO, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
          UpdateStatusCuponDTOValidation validationRules = new(LangCode);
            var validationResult=validationRules.Validate(updateStatusCuponDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return _cuponDAL.ChangeStatusCupon(updateStatusCuponDTO);
        }

        public IResult UpdateCupon(UpdateCuponDTO updateCuponDTO, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            UpdateCuponDTOValidation validateRules = new(LangCode);
            var validationResult = validateRules.Validate(updateCuponDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return _cuponDAL.UpdateCupon(updateCuponDTO);
        }

        public IDataResult<GetCuponInfoDTO> CheckedCuponCode(string cuponCode)
        {
            if (string.IsNullOrEmpty(cuponCode))
                return new ErrorDataResult<GetCuponInfoDTO>(HttpStatusCode.BadRequest);
            return _cuponDAL.CheckedCuponCode(cuponCode);
        }
    }
}
