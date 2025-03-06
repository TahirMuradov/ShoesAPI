using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Core.Helpers;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.Entites.DTOs.WebUI;
using System.Text.RegularExpressions;

namespace Shoes.Bussines.Concrete
{
   public class HomeManager:IHomeService
    {
        private readonly IHomeDAL _homeDAL;
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
        public HomeManager(IHomeDAL homeDAL)
        {
            _homeDAL = homeDAL;
        }


        public IDataResult<GetHomeAllDataDTO> GetHomeAllData(string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            return _homeDAL.GetHomeAllData(LangCode);
        }
    }
}
