using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.NewArriwalDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface INewArriwalService
    {
        public IDataResult<IQueryable<GetIsFeaturedCategoryDTO>> GetNewArriwalCategories(string LangCode);
        public IDataResult<IQueryable<GetNewArriwalProductDTO>> GetNewArriwalProducts(string LangCode);
    }
}
