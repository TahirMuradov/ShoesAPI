using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.PaymentMethodDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.PaymentMethodDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class PaymentMethodManager : IPaymentMethodService
    {
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
        private readonly IPaymentMethodDAL _paymentMethodDAL;

        public PaymentMethodManager(IPaymentMethodDAL paymentMethodDAL)
        {
            _paymentMethodDAL = paymentMethodDAL;
        }

        public IResult AddPaymentMethod(AddPaymentMethodDTO addPayment, string LangCode)
        {


            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            AddPaymentMethodDTOValidation validationRules = new(LangCode);
            var ValidationResult = validationRules.Validate(addPayment);
            if (!ValidationResult.IsValid)
            {
                List<string> Errors = ValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(messages: Errors, statusCode: HttpStatusCode.BadRequest);
            }
            return _paymentMethodDAL.AddPaymentMethod(addPayment);
        }

        public IResult DeletePaymentmethod(Guid Id)
        {
            if (Id == default)
                return new ErrorResult(statusCode: HttpStatusCode.BadRequest);
            return _paymentMethodDAL.DeletePaymentmethod(Id);
        }

        public async Task<IDataResult<PaginatedList<GetPaymentMethodDTO>>> GetAllPaymentmethodAsync(string LangCode, int page = 1)
        {
           
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            if (page < 1)
                page = 1;
            return await _paymentMethodDAL.GetAllPaymentmethodAsync(LangCode, page);

        }

        public IDataResult<GetPaymentMethodDTO> GetPaymentMethod(Guid Id, string LangCode)
        {
           
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            if (Id == default) return new ErrorDataResult<GetPaymentMethodDTO>(HttpStatusCode.BadRequest);
            return _paymentMethodDAL.GetPaymentMethod(Id, LangCode);

        }

        public IResult UpdatePaymenthod(UpdatePaymentMethodDTO updatePayment, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            UpdatePaymentMethodDTOValidation validationRules = new(LangCode);
            var ValidationResult = validationRules.Validate(updatePayment);
            if (!ValidationResult.IsValid)
            {
                List<string> errors = ValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(messages: errors, HttpStatusCode.BadRequest);
            }
            return _paymentMethodDAL.UpdatePaymenthod(updatePayment);
        }

        public IDataResult<GetPaymentMethodForUpdateDTO> GetPaymentMethodForUpdate(Guid Id)
        {
            if (Id == default)
                return new ErrorDataResult<GetPaymentMethodForUpdateDTO>(HttpStatusCode.BadRequest);
            return _paymentMethodDAL.GetPaymentMethodForUpdate(Id);
        }

        public IDataResult<IQueryable<GetPaymentMethodForUIDTO>> GetPaymentMethodForUI(string LangCode)
        {
            if (SupportedLaunguages.Contains(LangCode) || string.IsNullOrEmpty(LangCode))
                LangCode = DefaultLaunguage;
            return _paymentMethodDAL.GetPaymentMethodForUI(LangCode);
        }
    }
}
