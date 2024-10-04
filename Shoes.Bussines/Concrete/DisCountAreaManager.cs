using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.DisCountAreaDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class DisCountAreaManager : IDisCountAreaService
    {
        private readonly IDisCountAreaDAL _disCountAreaDAL;
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
        public DisCountAreaManager(IDisCountAreaDAL disCountAreaDAL)
        {
            _disCountAreaDAL = disCountAreaDAL;
        }

        public IResult AddDiscountArea(AddDisCountAreaDTO addDisCountAreaDTO, string culture)
        {
            if (!SupportedLaunguages.Contains(culture)||string.IsNullOrEmpty(culture))
                culture = DefaultLaunguage;
            AddDiscountAreaDTOValidation validationRules = new AddDiscountAreaDTOValidation(culture);
            var validationResult=validationRules.Validate(addDisCountAreaDTO);
            if (!validationResult.IsValid)
                     return new ErrorResult(messages:validationResult.Errors.Select(x=>x.ErrorMessage).ToList(),HttpStatusCode.BadRequest);
            return _disCountAreaDAL.AddDiscountArea(addDisCountAreaDTO);

        }

        public IResult Delete(Guid Id)
        {
            if (Id==default)
        return new ErrorResult(HttpStatusCode.BadRequest);
            return _disCountAreaDAL.Delete(Id);
        }

        public IDataResult<IQueryable<GetDisCountAreaDTO>> GetAllDisCountArea(string LangCode)
        {
            if (!SupportedLaunguages.Contains(LangCode)||string.IsNullOrEmpty(LangCode))
                LangCode = DefaultLaunguage;
            return _disCountAreaDAL.GetAllDisCountArea(LangCode);
        }

        public IDataResult<GETDisCountAreaForUpdateDTO> GetDisCountAreaForUpdate(Guid Id)
        {
            if (Id == default)
                return new ErrorDataResult<GETDisCountAreaForUpdateDTO>(HttpStatusCode.BadRequest);
            return _disCountAreaDAL.GetDisCountAreaForUpdate(Id);

        }

        public IResult UpdateDisCountArea(UpdateDisCountAreaDTO updateDisCountAreaDTO, string culture)
        {
            if (!SupportedLaunguages.Contains(culture) || string.IsNullOrEmpty(culture)) culture = DefaultLaunguage;
            UpdateDisCountAreaDTOValidation validationRules = new UpdateDisCountAreaDTOValidation(culture);
            var validationResult=validationRules.Validate(updateDisCountAreaDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return _disCountAreaDAL.UpdateDisCountArea(updateDisCountAreaDTO);

        }
    }
}
