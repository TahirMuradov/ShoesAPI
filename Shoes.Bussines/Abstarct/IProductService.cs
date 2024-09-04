﻿using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.ProductDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IProductService
    {
        public IDataResult<Guid> AddProduct(AddProductDTO addProductDTO,string LangCode);
        public IResult DeleteProduct(Guid Id);
        public Task<IResult> UpdateProductAsync(UpdateProductDTO updateProductDTO, string LangCode);
        public Task<IDataResult<PaginatedList<GetProductDashboardDTO>>> GetAllProductDashboardAsync(string LangCode, int page=1);
        public Task<IDataResult<PaginatedList<GetAllProductDTO>>> GetAllProductAsync(Guid subCategoryId, Guid SizeId, string LangCode, int Page = 1, decimal minPrice = 0, decimal maxPrice = 0);
        public IDataResult<GetDetailProductDTO> GetProductDetail(Guid Id, string LangCode);
        public IDataResult<GetDetailProductDashboardDTO> GetProductDetailDashboard(Guid id, string LangCode);
    }
}