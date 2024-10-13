using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Core.Helpers;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.Entites.DTOs.WebUI.NewArriwalDTOs;

namespace Shoes.Bussines.Concrete
{
    public class NewArriwalManager : INewArriwalService
    {
        private readonly INewArriwalAreaDAL _arriwalAreaDAL;
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
        public NewArriwalManager(INewArriwalAreaDAL arriwalAreaDAL)
        {
            _arriwalAreaDAL = arriwalAreaDAL;
        }

        public IDataResult<IQueryable<GetIsFeaturedCategoryDTO>> GetNewArriwalCategories(string LangCode)
        {
            if (!SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            return _arriwalAreaDAL.GetNewArriwalCategories(LangCode);
        }

        public IDataResult<IQueryable<GetNewArriwalProductDTO>> GetNewArriwalProducts(string LangCode)
        {
            if (!SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            return _arriwalAreaDAL.GetNewArriwalProducts(LangCode);
        }
    }
}
