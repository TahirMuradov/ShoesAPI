using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.ProductDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.ProductDTOs;
using System.Net;


namespace Shoes.Bussines.Concrete
{
    public class ProductManager : IProductService
    {
        protected string[] SupportedLaunguages
        {
            get
            {

                return ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();


            }
        }

        protected string DefaultLaunguage
        {
            get
            {
                return ConfigurationHelper.config.GetSection("SupportedLanguage:Default").Get<string>();
            }
        }
        private readonly IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public async Task<IResult> AddProductAsync(AddProductDTO addProductDTO,string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            AddProductDTOValidatior validationRules=new AddProductDTOValidatior(LangCode);
            var validationResult = validationRules.Validate(addProductDTO);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorDataResult<Guid>(errors, HttpStatusCode.BadRequest);
                
            }
            return await _productDAL.AddProductAsync(addProductDTO);

        }

        public IResult DeleteProduct(Guid Id)
        {
            if (Id == default)
                return new ErrorResult(HttpStatusCode.BadRequest);
            return _productDAL.DeleteProduct(Id);
        }

        public async Task<IDataResult<PaginatedList<GetAllProductDTO>>> GetAllProductAsync(Guid subCategoryId,Guid CategoryId, Guid SizeId, string LangCode, int Page = 1, decimal minPrice = 0, decimal maxPrice = 0)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            if (Page<=0)
                Page = 1;
         if(minPrice<0)
                minPrice = 0;
         if(maxPrice<0)
                maxPrice = 0;
         
                return await _productDAL.GetAllProductAsync(subCategoryId,CategoryId ,SizeId, LangCode, Page, minPrice, maxPrice);
        }

        public async Task<IDataResult<PaginatedList<GetProductDashboardDTO>>> GetAllProductDashboardAsync(string LangCode, int page=1)
        {
           if(string.IsNullOrEmpty(LangCode)|| !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
           if(page<=0)
                page = 1;
           return await _productDAL.GetAllProductDashboardAsync(LangCode, page);
        }

        public IDataResult<GetDetailProductDTO> GetProductDetail(Guid? Id, string LangCode)
        {
            if (Id==default||Id is null) return new ErrorDataResult<GetDetailProductDTO>(HttpStatusCode.BadRequest);
           if (string.IsNullOrEmpty(LangCode)|| !SupportedLaunguages.Contains(LangCode))
                LangCode= DefaultLaunguage;
           return _productDAL.GetProductDetail(Id, LangCode);
        }

        public IDataResult<GetDetailProductDashboardDTO> GetProductDetailDashboard(Guid id)
        {
            if (id == default) return new ErrorDataResult<GetDetailProductDashboardDTO>(HttpStatusCode.BadRequest);
         
            return _productDAL.GetProductDetailDashboard(id);
        }

        public async Task<IResult> UpdateProductAsync(UpdateProductDTO updateProductDTO, string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            UpdateProductDTOValidator validationRules =new UpdateProductDTOValidator(LangCode);
            var validationResult= await validationRules.ValidateAsync(updateProductDTO);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(errors, HttpStatusCode.BadRequest);
            }
            return await _productDAL.UpdateProductAsync(updateProductDTO);

        }

        public IDataResult<IQueryable<GetAllProductForSelectDTO>> GetAllProductForSelect()
        {
          return _productDAL.GetAllProductForSelect();
        }
    }
}
