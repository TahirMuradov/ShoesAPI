using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;

namespace Shoes.DataAccess.Abstarct.WebUI
{
    public interface ITopCategoryAreaDAL
    {
        public Task<IResult> AddTopCategoryAreaAsync(AddTopCategoryAreaDTO addTopCategoryAreaDTO );
        public Task<IResult> UpdateTopCategoryAreaAsync(UpdateTopCategoryAreaDTO updateTopCategoryAreaDTO);
        public IResult RemoveTopCategoryArea(Guid Id);
        public Task< IDataResult<PaginatedList<GetTopCategoryAreaDTO>>> GetTopCategoryAreaAsync(string LangCode,int page );
        public IDataResult<IQueryable<GetTopCategoryAreaForUIDTO>>GetTopCategoryAreaForUI(string LangCode);
        public IDataResult<GetTopCategoryAreaForUpdateDTO> GetTpcategoryAreaForUpdate(Guid Id);

    }
}
