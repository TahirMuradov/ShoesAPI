using Microsoft.EntityFrameworkCore;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites.DTOs.WebUI;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;
using Shoes.Entites.DTOs.WebUI.NewArriwalDTOs;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete.WebUI
{
    public class EFHomeDAL : IHomeDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFHomeDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IDataResult<GetHomeAllDataDTO> GetHomeAllData(string LangCode)
        {
            var context = _appDBContext;
            IQueryable<GetDisCountAreaUiDTO> DisCountAreas=context.DisCountAreas.AsNoTracking().Select(x=>new GetDisCountAreaUiDTO
            {
             
                Title = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Title,
                Description = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Description,
                
            });
            IQueryable<GetHomeSliderItemForUIDTO> HomeSliderItems=context.HomeSliderItems.AsNoTracking().Select(x => new GetHomeSliderItemForUIDTO
            {
                Title = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Title,
                Description = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Description,
               ImageUrl=x.BackgroundImageUrl
            });

            IQueryable<GetTopCategoryAreaForUIDTO> TopCategoryAreas=context.TopCategoryAreas.AsNoTracking().AsSplitQuery().Select(x => new GetTopCategoryAreaForUIDTO
            {
                Title = x.TopCategoryAreaLanguages.FirstOrDefault(y => y.LangCode == LangCode).Title,
                Description = x.TopCategoryAreaLanguages.FirstOrDefault(y => y.LangCode == LangCode).Description,
                PictureUrl=x.ImageUrl,
                CategoryName = x.SubCategory.SubCategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content
            });
            IQueryable<GetNewArriwalProductDTO> NewArriwalProducts = context.Products.AsNoTracking().Select(x => new GetNewArriwalProductDTO
            {
                Id = x.Id,
                Title = x.ProductLanguages.FirstOrDefault(y => y.LangCode == LangCode).Title,
                Category = x.SubCategories.Select(x => new GetIsFeaturedCategoryDTO
                {
                    CategoryName = x.SubCategory.SubCategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content,
                    CategoryId = x.SubCategoryId
                }).ToList(),
                DisCount = x.DiscountPrice,
                Price = x.Price,
                ImgUrls = x.Pictures.Select(x => x.Url).ToList()

            });
            IQueryable<GetIsFeaturedCategoryDTO> NewArriwalCategory=context.Categories.AsNoTracking().Where(x=>x.IsFeatured).AsQueryable().Select(x => new GetIsFeaturedCategoryDTO
            {
                CategoryId = x.Id,
                CategoryName = x.CategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content
            });
            return new SuccessDataResult<GetHomeAllDataDTO>(response: new GetHomeAllDataDTO
            {
                DisCountAreas = DisCountAreas,
                HomeSliderItems = HomeSliderItems,
                TopCategoryAreas = TopCategoryAreas,
                NewArriwalProducts = NewArriwalProducts,
                IsFeaturedCategorys= NewArriwalCategory
            },HttpStatusCode.OK);


        }
    }
}
