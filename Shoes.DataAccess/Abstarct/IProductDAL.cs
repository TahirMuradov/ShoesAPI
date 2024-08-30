using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.ProductDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface IProductDAL
    {
        public IResult AddProduct(AddProductDTO addProductDTO);
        public IResult DeleteProduct(Guid Id);
        public IResult UpdateProduct(UpdateProductDTO updateProductDTO);
        public Task<IDataResult<PaginatedList<GetProductDashboardDTO>>> GetAllProductDashboard(string LangCode,int page);
        public Task<IDataResult<PaginatedList<GetAllProductDTO>>> GetAllProduct(string LangCode ,int Page);
        public IDataResult<GetDetailProductDTO> GetProductDetail(Guid Id, string LangCode);
        public IDataResult<GetDetailProductDashboardDTO> GetProductDetailDashboard(Guid id,string LangCode);


    }
}
