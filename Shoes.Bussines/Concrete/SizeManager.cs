﻿using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.SizeDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.SizeDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class SizeManager : ISizeService
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
        private readonly ISizeDAL _sizeDAL;

        public SizeManager(ISizeDAL sizeDAL)
        {
            _sizeDAL = sizeDAL;
        }

        public IResult AddSize(AddSizeDTO addSizeDTO, string langCode)
        {
            if (string.IsNullOrEmpty(langCode) || !SupportedLaunguages.Contains(langCode))
                langCode = DefaultLaunguage;
            AddSizeDTOValidation validationRules = new AddSizeDTOValidation(langCode);
            var result=validationRules.Validate(addSizeDTO);
            if (!result.IsValid) { 
                List<string> errors=result.Errors.Select(x=>x.ErrorMessage).ToList();
                return new ErrorResult(messages: errors, statusCode: HttpStatusCode.BadRequest); 
            
            }
            return _sizeDAL.AddSize(addSizeDTO);

        }

        public IResult DeleteSize(Guid Id)
        {
            if (Id == default)
                return new ErrorResult(HttpStatusCode.BadRequest);
            return _sizeDAL.DeleteSize(Id);
        }

        public async Task<IDataResult<PaginatedList<GetSizeDTO>>> GetAllSizeForTableAsync(int page)
        {
          return await _sizeDAL.GetAllSizeForTableAsync(page);
        }

        public IDataResult<GetSizeForUpdateDTO> GetSize(Guid Id)
        {
            if (Id == default)
                return new ErrorDataResult<GetSizeForUpdateDTO>(HttpStatusCode.BadRequest);
            return _sizeDAL.GetSize(Id);

        }

        public IResult UpdateSize(UpdateSizeDTO updateSizeDTO, string langCode)
        {
            if (string.IsNullOrEmpty(langCode) || !SupportedLaunguages.Contains(langCode))
                langCode = DefaultLaunguage;
            SizeUpdateDTOValidation validationRules = new(langCode);
            var validationResult=validationRules.Validate(updateSizeDTO);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(messages:errors,statusCode: HttpStatusCode.BadRequest);
            }
            return _sizeDAL.UpdateSize(updateSizeDTO);
        }

        public IDataResult<IQueryable<GetSizeForUpdateDTO>> GetAllSize()
        {
            return _sizeDAL.GetAllSize();   
        }
    }
}
