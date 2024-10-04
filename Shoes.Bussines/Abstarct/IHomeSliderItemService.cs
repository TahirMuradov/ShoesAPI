using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IHomeSliderItemService
    {
        public Task<IResult> AddHomeSliderItemAsync(AddHomeSliderItemDTO addHomeSliderItemDTO,string culture);
        public Task<IResult> UpdateHomeSliderItemAsync(UpdateHomeSliderItemDTO updateHomeSliderItemDTO,string culture);
        public IResult DeleteHomeSliderItem(Guid Id);
        public Task<IDataResult<PaginatedList<GetHomeSliderItemDTO>>> GetAllHomeSliderAsync(string LangCode, int page);
        public IDataResult<GetHomeSliderItemForUpdateDTO> GetHomeSliderItemForUpdate(Guid Id);
        public IDataResult<IQueryable<GetHomeSliderItemForUIDTO>> GetHomeSliderItemForUI(string LangCode);

    }
}
