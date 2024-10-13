using Microsoft.EntityFrameworkCore;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites.DTOs.WebUI.NewArriwalDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete.WebUI
{
    public class EFNewArriwalDAL : INewArriwalAreaDAL
    {
        private readonly AppDBContext _dbContext;
        public EFNewArriwalDAL(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDataResult<IQueryable<GetIsFeaturedCategoryDTO>> GetNewArriwalCategories(string LangCode)
        {
            var query = _dbContext.Categories.AsNoTracking().AsSplitQuery().Where(x => x.IsFeatured).Select(x => new GetIsFeaturedCategoryDTO
            {
                CategoryId = x.Id,
                CategoryName = x.CategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content
            });
            return new SuccessDataResult<IQueryable<GetIsFeaturedCategoryDTO>>(statusCode: System.Net.HttpStatusCode.OK,response: query);
        }

        public IDataResult<IQueryable<GetNewArriwalProductDTO>> GetNewArriwalProducts(string LangCode)
        {
            var query = _dbContext.Products.AsNoTracking().AsSplitQuery().Where(x => x.SubCategories.Any(y => y.SubCategory.Category.IsFeatured)).Select(y => new GetNewArriwalProductDTO
            {
                Id = y.Id,
                DisCount = y.DiscountPrice,
                ImgUrls = y.Pictures.Select(z => z.Url).ToList(),
                Price = y.Price,
                Category = y.SubCategories.Select(x => new GetIsFeaturedCategoryDTO
                {
                    CategoryId=x.SubCategory.CategoryId,
                    CategoryName=x.SubCategory.Category.CategoryLanguages.FirstOrDefault(z=>z.LangCode==LangCode).Content,
                    
                }).ToList(),
                Title = y.ProductLanguages.FirstOrDefault(x => x.LangCode == LangCode).Title

            });
            return new SuccessDataResult<IQueryable<GetNewArriwalProductDTO>>(response: query, HttpStatusCode.OK);
        }
    }
}
