using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.WebUI.NewArriwalDTOs;

namespace Shoes.DataAccess.Abstarct.WebUI
{
    public interface INewArriwalAreaDAL
    {
        public IDataResult<IQueryable< GetIsFeaturedCategoryDTO>> GetNewArriwalCategories(string LangCode);
        public IDataResult<IQueryable<GetNewArriwalProductDTO>>GetNewArriwalProducts(string LangCode);
    }
}
