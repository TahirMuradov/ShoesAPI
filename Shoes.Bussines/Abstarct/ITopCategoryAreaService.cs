using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface ITopCategoryAreaService
    {
        public Task<IResult> AddTopCategoryAreaAsync(AddTopCategoryAreaDTO addTopCategoryAreaDTO,string culture);
        public Task<IResult> UpdateTopCategoryAreaAsync(UpdateTopCategoryAreaDTO updateTopCategoryAreaDTO,string culture);
        public IResult RemoveTopCategoryArea(Guid Id);
        public Task<IDataResult<PaginatedList<GetTopCategoryAreaDTO>>> GetTopCategoryAreaAsync(string LangCode, int page);
        public IDataResult<IQueryable<GetTopCategoryAreaForUIDTO>> GetTopCategoryAreaForUI(string LangCode);
        public IDataResult<GetTopCategoryAreaForUpdateDTO> GetTopcategoryAreaForUpdate(Guid Id);
    }
}
