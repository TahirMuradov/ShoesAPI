using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.ShippingMethodDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFShippingMethodDAL : IShippingMethodDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFShippingMethodDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IResult AddShippingMethod(AddShippingMethodDTO addShipping)
        {
            try
            {
                ShippingMethod shippingMethod = new ShippingMethod()
                {
                    discountPrice = addShipping.discountPrice,
                    price = addShipping.price,

                };
                _appDBContext.ShippingMethods.Add(shippingMethod);
                foreach (var item in addShipping.LangContent)
                {
                    ShippingMethodLanguage shippingMethodLanguage = new()
                    {
                        Content = item.Value,
                        ShippingMethodId = shippingMethod.Id,
                        LangCode = item.Key,
                        
                    };
                    _appDBContext.ShippingMethodLanguages.Add(shippingMethodLanguage);
                }
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message,statusCode:HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteShippingMethod(Guid Id)
        {
            var checekedData=_appDBContext.ShippingMethods.FirstOrDefault(x => x.Id == Id);
            if (checekedData is null)
           return new ErrorResult(HttpStatusCode.NotFound);
            _appDBContext.ShippingMethods.Remove(checekedData);
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IDataResult<PaginatedList<GetShippingMethodDTO>>> GetAllShippingMethodAsync(string LangCode, int page = 1)
        {

            IQueryable<GetShippingMethodDTO> queryData = _appDBContext.ShippingMethods.AsNoTracking().AsSplitQuery().Select(x => new GetShippingMethodDTO
            {
                Id = x.Id,
                Content = x.ShippingMethodLanguages.FirstOrDefault(x => x.LangCode == LangCode).Content,
                disCount = x.discountPrice,
                Price=x.price,
                

            });
            var resultData = await PaginatedList<GetShippingMethodDTO>.CreateAsync(queryData, page, 10);
            return new SuccessDataResult<PaginatedList<GetShippingMethodDTO>>(response:resultData,HttpStatusCode.OK);
        }

        public IDataResult<GetShippingMethodDTO> GetShippingMethod(Guid Id, string LangCode)
        {
            ShippingMethod getShipping = _appDBContext.ShippingMethods.AsNoTracking().Include(x=>x.ShippingMethodLanguages).FirstOrDefault(x => x.Id == Id);
            if (getShipping is null) return new ErrorDataResult<GetShippingMethodDTO>(HttpStatusCode.NotFound);
            return new SuccessDataResult<GetShippingMethodDTO>(response:new GetShippingMethodDTO
            {
                Id=getShipping.Id,
                Content=getShipping.ShippingMethodLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content,
                Price=getShipping.price,
                disCount=getShipping.discountPrice,
                
            },HttpStatusCode.OK);
        }

        public IResult UpdateShippingMethod(UpdateShippingMethodDTO updateShipping)
        {
            try
            {
                ShippingMethod shippingMethod=_appDBContext.ShippingMethods.Include(x=>x.ShippingMethodLanguages).FirstOrDefault(x=>x.Id==updateShipping.Id);
                if (shippingMethod is null) return new ErrorResult(HttpStatusCode.NotFound);
                foreach (var content in updateShipping.Lang)
                {
                    ShippingMethodLanguage shippingMethodLanguageChecked = shippingMethod.ShippingMethodLanguages.FirstOrDefault(x => x.LangCode == content.Key);
                    if (shippingMethodLanguageChecked is null)
                    {
                        ShippingMethodLanguage newLang  = new ShippingMethodLanguage()
                        {
                            LangCode = content.Key,
                            Content = content.Value,
                            ShippingMethodId = shippingMethod.Id,

                        };
                        _appDBContext.ShippingMethodLanguages.Add(newLang);
                    }
                    else
                    {
                        shippingMethodLanguageChecked.Content = content.Value;
                        _appDBContext.ShippingMethodLanguages.Update(shippingMethodLanguageChecked);
                    }

                }

                if (updateShipping.discountPrice>=0 && shippingMethod.discountPrice!=updateShipping.discountPrice)
                
                    shippingMethod.discountPrice = updateShipping.discountPrice;
              
                if(updateShipping.price >0 &&  shippingMethod.price!=updateShipping.price)
                    shippingMethod.price = updateShipping.price;
                _appDBContext.ShippingMethods.Update(shippingMethod);
                _appDBContext.SaveChanges();


                return new SuccessResult(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}
