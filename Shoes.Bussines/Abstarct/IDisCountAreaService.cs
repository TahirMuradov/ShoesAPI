using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IDisCountAreaService
    {
        public IResult AddDiscountArea(AddDisCountAreaDTO addDisCountAreaDTO,string culture);
        public IResult UpdateDisCountArea(UpdateDisCountAreaDTO updateDisCountAreaDTO,string culture);
        public IDataResult<GETDisCountAreaForUpdateDTO> GetDisCountAreaForUpdate(Guid Id);
        public IDataResult<IQueryable<GetDisCountAreaDTO>> GetAllDisCountArea(string LangCode);
        public Task<IDataResult<PaginatedList<GetDisCountAreaDTO>>> GetAllDisCountForTableAsync(string LangCode, int page);
        public IResult Delete(Guid Id);
    }
}
