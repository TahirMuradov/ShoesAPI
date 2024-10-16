using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.PaymentMethodDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFPaymentMethodDAL : IPaymentMethodDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFPaymentMethodDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IResult AddPaymentMethod(AddPaymentMethodDTO addPayment)
        {
            try
            {
                PaymentMethod paymentMethod = new PaymentMethod()
                {
                    IsApi=addPayment.IsApi,
                    
                };
                _appDBContext.PaymentMethods.Add(paymentMethod);    
                foreach (var item in addPayment.Lang)
                {
                    PaymentMethodLanguage paymentMethodLanguage = new PaymentMethodLanguage()
                    {
                        PaymentMethodId = paymentMethod.Id,
                        LangCode = item.Key,
                        Content = item.Value
                    };

                    _appDBContext.PaymentMethodLanguages.Add(paymentMethodLanguage);
                }
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

              return new ErrorResult(message:ex.Message,statusCode:HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<PaginatedList<GetPaymentMethodDTO>>> GetAllPaymentmethodAsync(string LangCode,int page=1)
        {
            try
            {
                var PaymentMethodaQuery=_appDBContext.PaymentMethods.AsNoTracking().AsSplitQuery().Select(x=>new GetPaymentMethodDTO
                {Id=x.Id,
                    Content = x.PaymentMethodLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content,
                    IsApi=x.IsApi,
                    
                });
                var resultData=await PaginatedList<GetPaymentMethodDTO>.CreateAsync(PaymentMethodaQuery, page,10);
                return new SuccessDataResult<PaginatedList<GetPaymentMethodDTO>>(response:resultData,HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

            return new ErrorDataResult<PaginatedList<GetPaymentMethodDTO>>(message:ex.Message,statusCode: HttpStatusCode.BadRequest);    
            }
        }

        public IDataResult<GetPaymentMethodDTO> GetPaymentMethod(Guid Id, string LangCode)
        {
            try
            {
                var chececkedData = _appDBContext.PaymentMethods.Include(x=>x.PaymentMethodLanguages).FirstOrDefault(x => x.Id == Id);
                if (chececkedData is null)
                    return new ErrorDataResult<GetPaymentMethodDTO>(statusCode: HttpStatusCode.NotFound);
                return new SuccessDataResult<GetPaymentMethodDTO>(response: new GetPaymentMethodDTO
                {
                    Id = Id,
                    Content=chececkedData.PaymentMethodLanguages.FirstOrDefault(x=>x.LangCode.ToLower()==LangCode.ToLower())?.Content
                },statusCode:HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetPaymentMethodDTO>(message:ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult UpdatePaymenthod(UpdatePaymentMethodDTO updatePayment)
        {
            try
            {
                var checkedMethod = _appDBContext.PaymentMethods.Include(x=>x.PaymentMethodLanguages).FirstOrDefault(x => x.Id == updatePayment.Id);
                if (checkedMethod is null)
                    return new ErrorResult(HttpStatusCode.NotFound);
                checkedMethod.IsApi=updatePayment.IsApi;
                foreach (var newContent in updatePayment.Lang)
                {
                    var checkedLangCode = checkedMethod.PaymentMethodLanguages.FirstOrDefault(x => x.LangCode == newContent.Key);
                    if (checkedLangCode is null)
                    {
                        PaymentMethodLanguage NewLaunguage = new PaymentMethodLanguage()
                        {
                            Content = newContent.Value,
                            LangCode = newContent.Key,
                            PaymentMethodId=checkedMethod.Id
                        };
                        _appDBContext.PaymentMethodLanguages.Add(NewLaunguage);

                    }else
                    {

                    checkedLangCode.Content = newContent.Value;
                    _appDBContext.PaymentMethodLanguages.Update(checkedLangCode);
                    }
                }
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
   
    public IResult DeletePaymentmethod(Guid Id)
        {
            try
            {
                var checekdData=_appDBContext.PaymentMethods.FirstOrDefault(x => x.Id == Id);
                if (checekdData is null)
              return new ErrorResult(statusCode:HttpStatusCode.BadRequest);
                _appDBContext.PaymentMethods.Remove(checekdData);
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<GetPaymentMethodForUpdateDTO> GetPaymentMethodForUpdate(Guid Id)
        {
          var checekdPaymentMethod=_appDBContext.PaymentMethods.Include(x=>x.PaymentMethodLanguages).FirstOrDefault(x=>x.Id == Id);
            if(checekdPaymentMethod is null)
                return new ErrorDataResult<GetPaymentMethodForUpdateDTO>(HttpStatusCode.NotFound);
            return new SuccessDataResult<GetPaymentMethodForUpdateDTO>(response: new GetPaymentMethodForUpdateDTO()
            {
                Id = checekdPaymentMethod.Id,
                IsApi = checekdPaymentMethod.IsApi,
                Content = checekdPaymentMethod.PaymentMethodLanguages.Select(x => new KeyValuePair<string, string>(x.LangCode, x.Content)).ToDictionary()
            }, HttpStatusCode.OK);
        }

        public IDataResult<IQueryable<GetPaymentMethodForUIDTO>> GetPaymentMethodForUI(string LangCode)
        {
            var query = _appDBContext.PaymentMethods.AsNoTracking().AsSplitQuery().Select(x => new GetPaymentMethodForUIDTO
            {
                Id = x.Id,
                Content = x.PaymentMethodLanguages.FirstOrDefault(y => y.LangCode == LangCode).LangCode,
                IsApi=x.IsApi
                
            });
            return new SuccessDataResult<IQueryable<GetPaymentMethodForUIDTO>>(response:query, HttpStatusCode.OK);
        }
    }
}
