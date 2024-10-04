using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;

namespace Shoes.DataAccess.Abstarct.WebUI
{
    public interface IHomeSliderItemDAL
    {
        public Task<IResult> AddHomeSliderItemAsync(AddHomeSliderItemDTO addHomeSliderItemDTO);
        public Task<IResult> UpdateHomeSliderItemAsync(UpdateHomeSliderItemDTO updateHomeSliderItemDTO);
        public IResult DeleteHomeSliderItem(Guid Id);
        public Task< IDataResult<PaginatedList<GetHomeSliderItemDTO>>> GetAllHomeSliderAsync(string LangCode, int page);
        public IDataResult<GetHomeSliderItemForUpdateDTO> GetHomeSliderItemForUpdate(Guid Id);
        public IDataResult<IQueryable<GetHomeSliderItemForUIDTO>> GetHomeSliderItemForUI(string LangCode);



    }
}
