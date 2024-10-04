using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;

namespace Shoes.DataAccess.Abstarct.WebUI
{
    public interface IDisCountAreaDAL
    {
        public IResult AddDiscountArea(AddDisCountAreaDTO addDisCountAreaDTO);
        public IResult UpdateDisCountArea(UpdateDisCountAreaDTO updateDisCountAreaDTO);
        public IDataResult<GETDisCountAreaForUpdateDTO> GetDisCountAreaForUpdate(Guid Id);
        public IDataResult<IQueryable< GetDisCountAreaDTO>> GetAllDisCountArea(string LangCode);
        public IResult Delete(Guid Id);
    }
}
