using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.FileHelper;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.ProductDTOs;

using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFProductDAL : IProductDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFProductDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IResult AddProduct(AddProductDTO addProductDTO)
        {
            try
            {
                Product product = new Product() 
                { 
                    DiscountPrice =addProductDTO.DiscountPrice,
                    Price =addProductDTO.Price,
                    ProductCode =addProductDTO.ProductCode,
                    
                    
                 };
                _appDBContext.Products.Add(product);
                Parallel.ForEach(addProductDTO.ProductName, i =>
                {
                    ProductLanguage productLanguage = new ProductLanguage()
                    {
                        ProductId = product.Id,
                        LangCode = i.Key,
                        Title = i.Value,
                        Description = addProductDTO.ProductName.GetValueOrDefault(i.Key),

                    };
                    _appDBContext.ProductLanguages.Add(productLanguage);
                });
                Parallel.ForEach(addProductDTO.Sizes, i =>
                {
                    if (i.Value == 0)
                        return;
                    bool sizeCheceked = _appDBContext.Sizes.AsNoTracking().Any(x => x.Id == i.Key);
                    if (!sizeCheceked) return;
                    SizeProduct sizeProduct = new SizeProduct()
                    {
                        ProductId = product.Id,
                        SizeId = i.Key,
                        StockCount = i.Value
                    };
                    _appDBContext.SizeProducts.Add(sizeProduct);
                });
                Parallel.ForEach(addProductDTO.SubCategories, i =>
                {
                    bool CheckedSubCategory = _appDBContext.SubCategories.AsNoTracking().Any(x => x.Id == i);
                    if (!CheckedSubCategory) return;
                    SubCategoryProduct subCategoryProduct = new SubCategoryProduct()
                    {
                        ProductId = product.Id,
                        SubCategoryId = i,

                    };
                    _appDBContext.SubCategoryProducts.Add(subCategoryProduct);
                });
               _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteProduct(Guid Id)
        {
         Product product=_appDBContext.Products.FirstOrDefault(x => x.Id == Id);
            if (product is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            _appDBContext.Products.Remove(product);
            _appDBContext.SaveChanges(); 
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IDataResult<PaginatedList<GetAllProductDTO>>> GetAllProduct(string LangCode,int page)
        {
            IQueryable<GetAllProductDTO> productQuery = _appDBContext.Products.AsSplitQuery().AsNoTracking().Select(x => new GetAllProductDTO
            {
                Id = x.Id,
                DisCount = x.DiscountPrice,
                Price = x.Price,
                ImgUrls = x.Pictures.Select(y => y.Url).ToList(),
                Title = x.ProductLanguages.FirstOrDefault(y => y.LangCode == LangCode).Title,

            });
            var resultData= await PaginatedList<GetAllProductDTO>.CreateAsync(productQuery,page,10);
           return new SuccessDataResult<PaginatedList<GetAllProductDTO>>(response:resultData,HttpStatusCode.OK);    
        }

        public async Task<IDataResult<PaginatedList<GetProductDashboardDTO>>> GetAllProductDashboard(string LangCode, int page)
        {
            IQueryable<GetProductDashboardDTO> productQuery = _appDBContext.Products.AsSplitQuery().AsNoTracking().Select(x => new GetProductDashboardDTO
            {
               Id= x.Id,
               DisCount= x.DiscountPrice,
               Price= x.Price,
               ProductCode=x.ProductCode,
               SubCategory=x.SubCategories.Select(y=>y.SubCategory.SubCategoryLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content).ToList(),
               ProductTitle= x.ProductLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Title,
               SizeAndCount = x.SizeProducts.Select(ps => new KeyValuePair<int, int>(ps.Size.SizeNumber, ps.StockCount)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
               
            });
            var resultData = await PaginatedList<GetProductDashboardDTO>.CreateAsync(productQuery, page, 10);
            return new SuccessDataResult<PaginatedList<GetProductDashboardDTO>>(response: resultData, HttpStatusCode.OK);
        }

        public IDataResult<GetDetailProductDTO> GetProductDetail(Guid Id, string LangCode)
        {
            var product = _appDBContext.Products
        .AsNoTracking()
        .AsSplitQuery()
        .Where(p => p.Id == Id)
        .Select(p => new
        {
            p.Id,
            p.DiscountPrice,
            p.Price,
            Pictures = p.Pictures.Select(pic => pic.Url).ToList(),
            Languages = p.ProductLanguages.FirstOrDefault(pl => pl.LangCode == LangCode),
            SubCategoryNames = p.SubCategories
                .Select(sc => sc.SubCategory.SubCategoryLanguages.FirstOrDefault(scl => scl.LangCode == LangCode).Content)
                .ToList(),
            Sizes = p.SizeProducts
                .Select(sp => new GetProductSizeInfoDTO
                {
                    SizeId = sp.SizeId,
                    SizeNumber = sp.Size.SizeNumber,
                    StockCount = sp.StockCount
                }).ToList(),
            RelatedProducts = p.SubCategories
                .SelectMany(sc => sc.SubCategory.SubCategoryProducts
                    .Where(scp => scp.ProductId != p.Id)
                    .Select(scp => new GetRelatedProductDTO
                    {
                        Id = scp.ProductId,
                        DisCount = scp.Product.DiscountPrice,
                        ImgUrls = scp.Product.Pictures.Select(pic => pic.Url).ToList(),
                        Price = scp.Product.Price,
                        Title = scp.Product.ProductLanguages.FirstOrDefault(pl => pl.LangCode == LangCode).Title
                    })).ToList()
        })
        .FirstOrDefault();

            if (product == null)
                return new ErrorDataResult<GetDetailProductDTO>(HttpStatusCode.NotFound);

            var dto = new GetDetailProductDTO
            {
                Id = product.Id,
                DisCount = product.DiscountPrice,
                Price = product.Price,
                Title = product.Languages?.Title,
                Description = product.Languages?.Description,
                SubCategoryName = product.SubCategoryNames,
                ImgUrls = product.Pictures,
                Size = product.Sizes,
                RelatedProducts = product.RelatedProducts
            };

            return new SuccessDataResult<GetDetailProductDTO>(dto, HttpStatusCode.OK);
        }

        public IDataResult<GetDetailProductDashboardDTO> GetProductDetailDashboard(Guid id, string LangCode)
        {
            var product = _appDBContext.Products
           .AsNoTracking()
           .AsSplitQuery()
           .Where(p => p.Id == id)
           .Select(p => new GetDetailProductDashboardDTO
           {
             Id=  p.Id,
               ProductName = p.ProductLanguages.ToDictionary(pl => pl.LangCode, pl => pl.Title),
               Description = p.ProductLanguages.ToDictionary(pl => pl.LangCode, pl => pl.Description),
               Sizes = p.SizeProducts.ToDictionary(sp => sp.SizeId, sp => sp.Size.SizeNumber),
             DiscountPrice=  p.DiscountPrice,
              Price= p.Price,
             ProductCode=  p.ProductCode,
               SubCategories = p.SubCategories.Select(sc => sc.SubCategory.SubCategoryLanguages.FirstOrDefault(scl => scl.LangCode == LangCode).Content).ToList(),
               PictureUrls = p.Pictures.Select(pic => pic.Url).ToList()
           })
           .FirstOrDefault(x=>x.Id==id);

            if (product == null)
                return new ErrorDataResult<GetDetailProductDashboardDTO>(HttpStatusCode.NotFound);    

            return new SuccessDataResult<GetDetailProductDashboardDTO>(product, HttpStatusCode.OK);
        }

        public IResult UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            Product product = _appDBContext.Products.AsSplitQuery().FirstOrDefault(x => x.Id == updateProductDTO.Id);
            var pivot = _appDBContext.SubCategoryProducts.Where(x => x.ProductId == product.Id);
            if (product is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            if (updateProductDTO.CurrentPictureUrls is not null)
            {

                Parallel.ForEach(updateProductDTO.CurrentPictureUrls, url =>
                {
                    if (!product.Pictures.Any(x => x.Url == url))
                    {
                        FileHeleper.RemoveFile(url);
                    }
                });
            }
            if (updateProductDTO.NewPictures is not null)
            {
              List<string> newUrls=  FileHeleper.PhotoFileSaveRangeAsync(updateProductDTO.NewPictures);
                Parallel.ForEach(newUrls, url =>
                {
                    Picture picture = new Picture()
                    {
                        ProductId = product.Id,
                        Url = url,

                    };
                    _appDBContext.Pictures.Add(picture);
                });
                
            }
            if (updateProductDTO.ProductCode is not null)
                product.ProductCode = updateProductDTO.ProductCode;
          product.DiscountPrice = updateProductDTO.DiscountPrice;
            product.Price = updateProductDTO.Price;
            if (updateProductDTO.Sizes is not null)
            {
                Parallel.ForEach(updateProductDTO.Sizes, size =>
                {
                    var productSizeChecked = product.SizeProducts.FirstOrDefault(x => x.Size.SizeNumber == size.Key);
                    if (productSizeChecked != null)
                    {
                        productSizeChecked.StockCount = size.Value;
                        _appDBContext.SizeProducts.Update(productSizeChecked);
                    }
                    else
                    {
                    var checekdSize = _appDBContext.Sizes.AsSplitQuery().FirstOrDefault(x => x.SizeNumber == size.Key);
                        if(checekdSize != null)
                        {
                            SizeProduct sizeProduct = new SizeProduct()
                            {
                                ProductId = product.Id,
                                SizeId = checekdSize.Id,
                                StockCount = size.Value,
                            };
                            _appDBContext.SizeProducts.Add(sizeProduct);
                        }
                        else
                        {
                            Size newSize= new Size()
                            {
                                SizeNumber = size.Key,
                            };
                            _appDBContext.Sizes.Add(newSize);
                            SizeProduct sizeProduct = new SizeProduct()
                            {
                                ProductId = product.Id,

                                SizeId = newSize.Id,
                                StockCount = size.Value,
                            };
                            _appDBContext.SizeProducts.Add(sizeProduct);
                        }
                    }

               
                  

                });
            }

            if (updateProductDTO.Description is not null)
            {
                Parallel.ForEach(updateProductDTO.Description, i =>
                {
                    var checkedLangCode = product.ProductLanguages.FirstOrDefault(x => x.LangCode == i.Key);
                    if (checkedLangCode != null)
                    {
                        checkedLangCode.Description = i.Value;
                        checkedLangCode.Title = updateProductDTO.Description.GetValueOrDefault(i.Key);

                    }
                    else
                    {
                        ProductLanguage productLanguage = new ProductLanguage()
                        {
                            ProductId = product.Id,
                            Description = i.Value,
                            Title = updateProductDTO.Description.GetValueOrDefault(i.Key),
                            LangCode = i.Key,

                        };
                        _appDBContext.ProductLanguages.Add(productLanguage);
                    }
                });
            }
            if (updateProductDTO.SubCategoriesID is not null)
            {

                Parallel.ForEach(updateProductDTO.SubCategoriesID, i =>
                {
                    var ProductSubCategory = product.SubCategories.FirstOrDefault(x=>x.Id==i);
                    if (ProductSubCategory is  null)
                    {
                        SubCategoryProduct subCategoryProduct = new SubCategoryProduct()
                        {
                            ProductId = product.Id,
                            SubCategoryId = ProductSubCategory.Id
                        };
                        _appDBContext.SubCategoryProducts.Add(subCategoryProduct);
                       
                    }                
                
                });
                Parallel.ForEach(product.SizeProducts, i =>
                {
                    
                    var isDeleteSubCategoryProdutc = updateProductDTO.SubCategoriesID.FirstOrDefault(x => x == i.SizeId);
                    if (isDeleteSubCategoryProdutc == default)
                        _appDBContext.SizeProducts.Remove(i);

                });
            
                
            }
        _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
