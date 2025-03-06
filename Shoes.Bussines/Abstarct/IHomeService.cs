

using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI;

namespace Shoes.Bussines.Abstarct
{
  public  interface IHomeService
    {
        IDataResult<GetHomeAllDataDTO> GetHomeAllData(string LangCode);

    }
}
