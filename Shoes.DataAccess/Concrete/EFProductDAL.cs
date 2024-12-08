using iText.Commons.Bouncycastle.Asn1.Esf;
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

        public async Task<IResult> AddProductAsync(AddProductDTO addProductDTO)
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
                foreach (var i in addProductDTO.ProductName)
                {
                    ProductLanguage productLanguage = new ProductLanguage()
                    {
                        ProductId = product.Id,
                        LangCode = i.Key,
                        Title = i.Value,
                        Description = addProductDTO.Description.GetValueOrDefault(i.Key),

                    };
                 await   _appDBContext.ProductLanguages.AddAsync(productLanguage);
                }
                foreach (var i in addProductDTO.Sizes)
                {
                    if (i.Value == 0)
                        continue;
                    bool sizeCheceked =await _appDBContext.Sizes.AsNoTracking().AnyAsync(x => x.Id == i.Key);
                    if (!sizeCheceked) continue;
                    SizeProduct sizeProduct = new SizeProduct()
                    {
                        ProductId = product.Id,
                        SizeId = i.Key,
                        StockCount = i.Value
                    };
                   await _appDBContext.SizeProducts.AddAsync(sizeProduct);

                }
                foreach (var i in addProductDTO.SubCategories)
                {
                    bool CheckedSubCategory =await _appDBContext.SubCategories.AsNoTracking().AnyAsync(x => x.Id == i);
                    if (!CheckedSubCategory) continue;
                    SubCategoryProduct subCategoryProduct = new SubCategoryProduct()
                    {
                        ProductId = product.Id,
                        SubCategoryId = i,

                    };
                    await _appDBContext.SubCategoryProducts.AddAsync(subCategoryProduct);

                }
                List<string> photoUrls = await FileHelper.PhotoFileSaveRangeAsync(addProductDTO.Pictures);
                foreach (string url in photoUrls)
                {
                    Picture picture = new Picture()
                    {
                        ProductId = product.Id,
                        Url = url
                    };
                 await  _appDBContext.Pictures.AddAsync(picture);
                }
                
             await   _appDBContext.SaveChangesAsync();
                return new SuccessDataResult<Guid>(response:product.Id,HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<Guid>(message:ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteProduct(Guid Id)
        {
         Product product=_appDBContext.Products.Include(x=>x.Pictures).FirstOrDefault(x => x.Id == Id);
            if (product is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            FileHelper.RemoveFileRange(product.Pictures.Select(x=>x.Url).ToList());
            _appDBContext.Products.Remove(product);
            _appDBContext.SaveChanges(); 
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IDataResult<PaginatedList<GetAllProductDTO>>> GetAllProductAsync(Guid subCategoryId,Guid CategoryId, Guid SizeId,string LangCode,int page, decimal minPrice=0, decimal maxPrice = 0)
        {
            IQueryable<Product> productQuery = _appDBContext.Products.AsNoTracking().AsQueryable();
            if (subCategoryId != default)
            {
             
                productQuery = productQuery.Where(x => x.SubCategories.Any(y => y.SubCategoryId == subCategoryId));
            }
            if (CategoryId != default)
            {
                productQuery = productQuery.Where(x => x.SubCategories.Any(y => y.SubCategory.CategoryId == CategoryId));
            }
            if (SizeId != default)
            {
                productQuery = productQuery.Where(x => x.SizeProducts.Any(y => y.SizeId == SizeId));
            }
            if (minPrice > 0 || maxPrice > 0)
            {
                if (minPrice > 0)
                {
                    productQuery = productQuery.Where(x => x.Price >= minPrice);
                }

                if (maxPrice > 0)
                {
                    productQuery = productQuery.Where(x => x.Price <= maxPrice);
                }
            }
          IQueryable<GetAllProductDTO>  Result = productQuery.Select(x => new GetAllProductDTO
            {
                Id = x.Id,
                DisCount = x.DiscountPrice,
                Price = x.Price,
                ImgUrls = x.Pictures.Select(y => y.Url).ToList(),
                Title = x.ProductLanguages.FirstOrDefault(y => y.LangCode == LangCode).Title,
            });

            var resultData= await PaginatedList<GetAllProductDTO>.CreateAsync(Result, page,10);
           return new SuccessDataResult<PaginatedList<GetAllProductDTO>>(response:resultData,HttpStatusCode.OK);    
        }

        public async Task<IDataResult<PaginatedList<GetProductDashboardDTO>>> GetAllProductDashboardAsync(string LangCode, int page)
        {
            var productQuery = _appDBContext.Products.AsNoTracking().
                 Include(x => x.SizeProducts)
                 .Include(x => x.ProductLanguages)
                 .Include(x => x.Pictures)
                 .Include(x => x.SubCategories)
                 .ThenInclude(x => x.SubCategory.SubCategoryLanguages)
                 .AsQueryable().Select(x => new GetProductDashboardDTO
                 {
                     Id = x.Id,
                     PictureUrls = x.Pictures.Select(y => y.Url).ToList(),
 
                DisCount = x.DiscountPrice,
                     Price = x.Price,
                     ProductCode = x.ProductCode,
                     SubCategory = x.SubCategories.Select(y => y.SubCategory.SubCategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content).ToList(),
                     ProductTitle = x.ProductLanguages.FirstOrDefault(y => y.LangCode == LangCode).Title,
                     Sizes = x.SizeProducts.Select(ps => new GetProductSizeInfoDTO
                     {
                         SizeId = ps.SizeId,
                         SizeNumber = ps.Size.SizeNumber,
                         StockCount = ps.StockCount,
                     }).ToList(),

                 }) ;
            var resultData = await PaginatedList<GetProductDashboardDTO>.CreateAsync(productQuery, page, 10);
            return new SuccessDataResult<PaginatedList<GetProductDashboardDTO>>(response: resultData, HttpStatusCode.OK);
        }

        public IDataResult<IQueryable<GetAllProductForSelectDTO>> GetAllProductForSelect()
        {
         var query=_appDBContext.Products.Select(x=>new GetAllProductForSelectDTO
         {
             Id=x.Id,
             ProductCode=x.ProductCode,
         }).AsNoTracking();
            return new SuccessDataResult<IQueryable<GetAllProductForSelectDTO>>(query, HttpStatusCode.OK);
        }

        public IDataResult<GetDetailProductDTO> GetProductDetail(Guid Id, string LangCode)
        {
            var product = _appDBContext.Products
        .AsNoTracking()
        .AsSplitQuery()
        .Select(p => new
        {
            p.Id,
            p.DiscountPrice,
            p.Price,
            p.ProductCode,
         
          CategoryNames=p.SubCategories.Select(x=>new KeyValuePair<Guid,string>(x.SubCategory.CategoryId,x.SubCategory.Category.CategoryLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content)).ToDictionary(),
            SubCategoryNames = p.SubCategories
                .Select(sc =>new KeyValuePair<Guid,string>(sc.SubCategoryId,sc.SubCategory.SubCategoryLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content))
                .ToDictionary(),
            Pictures = p.Pictures.Select(pic => pic.Url).ToList(),
            Languages = p.ProductLanguages.FirstOrDefault(pl => pl.LangCode == LangCode),
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
        .FirstOrDefault(p => p.Id == Id);
       

            if (product == null)
                return new ErrorDataResult<GetDetailProductDTO>(HttpStatusCode.NotFound);

            var dto = new GetDetailProductDTO
            {
                Id = product.Id,
                DisCount = product.DiscountPrice,
                Price = product.Price,
                Title = product.Languages?.Title,
                Description = product.Languages?.Description,
                ProductCode=product.ProductCode,
                SubCategories = product.SubCategoryNames,
                ImgUrls = product.Pictures,
                Size = product.Sizes,
                RelatedProducts = product.RelatedProducts,
                Categories=product.CategoryNames,
              
            };

            return new SuccessDataResult<GetDetailProductDTO>(dto, HttpStatusCode.OK);
        }

        public IDataResult<GetDetailProductDashboardDTO> GetProductDetailDashboard(Guid id)
        {
            var product = _appDBContext.Products
           .AsNoTracking()
           .AsSplitQuery()
         
           .Select(p => new GetDetailProductDashboardDTO
           {
             Id=  p.Id,
               ProductName = p.ProductLanguages.Select(x=>new KeyValuePair<string,string>(x.LangCode,x.Title)).ToDictionary(),
               Description = p.ProductLanguages.Select(x => new KeyValuePair<string, string>(x.LangCode, x.Description)).ToDictionary(),
               Sizes = p.SizeProducts.Select(x=>new GetProductSizeInfoDTO
               {
                   SizeId = x.SizeId,
                   SizeNumber=x.Size.SizeNumber,
                   StockCount=x.StockCount
               }).ToList(),
             DiscountPrice=  p.DiscountPrice,
              Price= p.Price,
             ProductCode=  p.ProductCode,
               SubCategories = p.SubCategories.Select(sc => sc.SubCategory.Id).ToList(),
               PictureUrls = p.Pictures.Select(pic => pic.Url).ToList()
           })
           .FirstOrDefault(x=>x.Id==id);

            if (product == null)
                return new ErrorDataResult<GetDetailProductDashboardDTO>(HttpStatusCode.NotFound);    

            return new SuccessDataResult<GetDetailProductDashboardDTO>(product, HttpStatusCode.OK);
        }

        public async Task< IResult> UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            Product product = await _appDBContext.Products
                .Include(x=>x.ProductLanguages)
                .Include(x=>x.Pictures)
                .Include(x=>x.SizeProducts)
                .Include(x=>x.SubCategories)
               .FirstOrDefaultAsync(x => x.Id == updateProductDTO.Id);
        
            if (product is null)
                return new ErrorResult(HttpStatusCode.NotFound);
         

            
                foreach (var url in product.Pictures)
                {
                    if (!updateProductDTO.CurrentPictureUrls.Any(x => x == url.Url))
                    {
                     bool result=   FileHelper.RemoveFile(url.Url);
                        if (result)
                        {
                            _appDBContext.Pictures.Remove(url);
                        }
                    }
                }
         
            if (updateProductDTO.NewPictures is not null)
            {
              List<string> newUrls=  await FileHelper.PhotoFileSaveRangeAsync(updateProductDTO.NewPictures);
           
                foreach (string url in newUrls)
                {
                    Picture picture = new Picture()
                    {
                        ProductId = product.Id,
                        Url = url,

                    };
                    await _appDBContext.Pictures.AddAsync(picture);
                }
            }
            if (updateProductDTO.ProductCode is not null)
                product.ProductCode = updateProductDTO.ProductCode;
          product.DiscountPrice = updateProductDTO.DiscountPrice;
            product.Price = updateProductDTO.Price;
            if (updateProductDTO.Sizes is not null)
            {             
                foreach (var size in updateProductDTO.Sizes)
                {
                    var productSizeChecked = product.SizeProducts.FirstOrDefault(x => x.SizeId == size.Key);
                    if (productSizeChecked != null)
                    {
                        productSizeChecked.StockCount = size.Value;
                        _appDBContext.SizeProducts.Update(productSizeChecked);
                    }
                    else
                    {
                        var checekdSize = _appDBContext.Sizes.AsSplitQuery().FirstOrDefault(x => x.Id == size.Key);
                        if (checekdSize != null)
                        {
                            SizeProduct sizeProduct = new SizeProduct()
                            {
                                ProductId = product.Id,
                                SizeId = checekdSize.Id,
                                StockCount = size.Value,
                            };
                            _appDBContext.SizeProducts.Add(sizeProduct);
                        }
                        //else
                        //{
                        //    Size newSize= new Size()
                        //    {
                        //        SizeNumber = size.Key,
                        //    };
                        //    _appDBContext.Sizes.Add(newSize);
                        //    SizeProduct sizeProduct = new SizeProduct()
                        //    {
                        //        ProductId = product.Id,

                        //        SizeId = newSize.Id,
                        //        StockCount = size.Value,
                        //    };
                        //    _appDBContext.SizeProducts.Add(sizeProduct);
                        //}
                    }
                }
            }

            if (updateProductDTO.Description is not null)
            {
                foreach(var i in updateProductDTO.Description)
                {
                    var checkedLangCode = product.ProductLanguages.FirstOrDefault(x => x.LangCode == i.Key);
                    if (checkedLangCode != null)
                    {
                        checkedLangCode.Description = i.Value;
                        checkedLangCode.Title = updateProductDTO.ProductName.GetValueOrDefault(i.Key);

                   _appDBContext.ProductLanguages.Update(checkedLangCode);
                    }
                    else
                    {
                        ProductLanguage productLanguage = new ProductLanguage()
                        {
                            ProductId = product.Id,
                            Description = i.Value,
                            Title = updateProductDTO.ProductName.GetValueOrDefault(i.Key),
                            LangCode = i.Key,

                        };
                        _appDBContext.ProductLanguages.Add(productLanguage);
                    }
                }
            
            }
            if (updateProductDTO.SubCategoriesID is not null)
            {

                foreach (var i in product.SubCategories)
                {
                    var SubCategoryid = updateProductDTO.SubCategoriesID.FirstOrDefault(x => x == i.SubCategoryId);
                    if (SubCategoryid ==default)
                    {
                        _appDBContext.SubCategoryProducts.Remove(i);   
                    }

                }
                foreach (var subCategoryId in updateProductDTO.SubCategoriesID)
                {
                    var subCategoryChecked = _appDBContext.SubCategories.FirstOrDefault(x => x.Id == subCategoryId);
                    if (subCategoryChecked is null)
                        continue;
                    var productSubCategoryCheceked = product.SubCategories.FirstOrDefault(x => x.SubCategoryId == subCategoryId);
                    if (productSubCategoryCheceked is not null)
                        continue;
                   SubCategoryProduct subCategoryProduct = new SubCategoryProduct()
                   {
                       ProductId=product.Id,
                       SubCategoryId= subCategoryChecked.Id
                   };
                    _appDBContext.SubCategoryProducts.Add(subCategoryProduct);
                }
            
          
                
            }
     await   _appDBContext.SaveChangesAsync();
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
