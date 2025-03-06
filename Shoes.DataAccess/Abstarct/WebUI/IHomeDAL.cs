using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI;

namespace Shoes.DataAccess.Abstarct.WebUI
{
  public  interface IHomeDAL
    {
        IDataResult<GetHomeAllDataDTO> GetHomeAllData(string LangCode);
    }
}
