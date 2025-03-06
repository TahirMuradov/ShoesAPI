using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;
using Shoes.Entites.DTOs.WebUI.NewArriwalDTOs;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;

namespace Shoes.Entites.DTOs.WebUI
{
  public  class GetHomeAllDataDTO
    {
        public IQueryable<GetDisCountAreaUiDTO> DisCountAreas { get; set; }
        public IQueryable<GetHomeSliderItemForUIDTO> HomeSliderItems { get; set; }
        public IQueryable<GetTopCategoryAreaForUIDTO> TopCategoryAreas { get; set; }
        public IQueryable<GetNewArriwalProductDTO> NewArriwalProducts { get; set; }
        public IQueryable<GetIsFeaturedCategoryDTO> IsFeaturedCategorys { get; set; }
    }
}
