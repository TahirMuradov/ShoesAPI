using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.ShippingMethodDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.ShippingMethodDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class ShippingMethodManager : IShippingMethodService
    {
        private readonly IShippingMethodDAL _shippingMethodDAL;

        public ShippingMethodManager(IShippingMethodDAL shippingMethodDAL)
        {
            _shippingMethodDAL = shippingMethodDAL;
        }
        protected string[] SupportedLaunguages
        {
            get
            {

                return ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();


            }
        }

        protected string DefaultLaunguage
        {
            get
            {
                return ConfigurationHelper.config.GetSection("SupportedLanguage:Default").Get<string>();
            }
        }
        public IResult AddShippingMethod(AddShippingMethodDTO addShipping, string LangCode)
        {
            if (!SupportedLaunguages.Contains(LangCode))
               LangCode = DefaultLaunguage;
           
            AddShippingMethodDTOValidation validationRules = new AddShippingMethodDTOValidation(LangCode);
            var validationResult = validationRules.Validate(addShipping);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(messages: errors, HttpStatusCode.BadRequest);
            }
            return _shippingMethodDAL.AddShippingMethod(addShipping);
        }

        public IResult DeleteShippingMethod(Guid Id)
        {
            if (Id==default)
          return new ErrorResult(HttpStatusCode.BadRequest);
            return _shippingMethodDAL.DeleteShippingMethod(Id);
        }

        public async Task<IDataResult<PaginatedList<GetShippingMethodDTO>>> GetAllShippingMethodAsync(string LangCode, int page = 1)
        {
            if (!SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            if (page<=0)
           page=1;
            return await _shippingMethodDAL.GetAllShippingMethodAsync(LangCode, page);

        }

        public IDataResult<GetShippingMethodDTO> GetShippingMethod(Guid Id, string LangCode)
        {
            if (Id == default)
                return new ErrorDataResult<GetShippingMethodDTO>(HttpStatusCode.BadRequest);
            if(!SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            return _shippingMethodDAL.GetShippingMethod(Id, LangCode);
        }

        public IDataResult<IQueryable<GetShippingMethodForUIDTO>> GetShippingMethodForUI(string LangCode)
        {
            if (SupportedLaunguages.Contains(LangCode) || string.IsNullOrEmpty(LangCode))
                LangCode = DefaultLaunguage;
            return _shippingMethodDAL.GetShippingMethodForUI(LangCode);
        }

        public IDataResult<GetShippingMethodForUpdateDTO> GetShippingMethodForUpdate(Guid Id)
        {
            if (Id == default)
                return new ErrorDataResult<GetShippingMethodForUpdateDTO>(HttpStatusCode.BadRequest);
            return _shippingMethodDAL.GetShippingMethodForUpdate(Id);
        }

        public IResult UpdateShippingMethod(UpdateShippingMethodDTO updateShipping, string LangCode)
        {
            if (!SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            UpdateShippingMethodDTOValidation validationRules = new UpdateShippingMethodDTOValidation(LangCode);
           var validationResult= validationRules.Validate(updateShipping);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(errors, HttpStatusCode.BadRequest);
            }
            return _shippingMethodDAL.UpdateShippingMethod(updateShipping);
        }
    }
}
