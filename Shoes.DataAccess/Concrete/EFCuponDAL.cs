using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.CuponDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFCuponDAL : ICuponDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFCuponDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IDataResult<string> AddSpecificCuponForCategory(AddCuponForCategoryDTO addCuponForCategoryDTO)
        {



            var checekdCategory = _appDBContext.Categories.Include(y => y.CategoryCupons).FirstOrDefault(x => x.Id == addCuponForCategoryDTO.CategoryId);
            if (checekdCategory is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdCategory.CategoryCupons.FirstOrDefault(x => x.Cupon.DisCountPercent == addCuponForCategoryDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Cupon.Code, HttpStatusCode.AlreadyReported);

            Random random = new Random();
            Cupon cupon = new Cupon()
            {
                DisCountPercent = addCuponForCategoryDTO.DisCountPercent,
                Code = GenerateCouponCode.GenerateCouponCodeFromGuid(),
            };
            _appDBContext.Cupons.Add(cupon);
            CategoryCupon categoryCupon = new CategoryCupon()
            {
                CategoryId = checekdCategory.Id,
                CuponId = cupon.Id,
                IsActive = true,
            };
            _appDBContext.CategoryCupons.Add(categoryCupon);
            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);

        }

        public IDataResult<string> AddSpecificCuponForProduct(AddCuponForProductDTO addCuponForProductDTO)
        {
            var checekdProduct = _appDBContext.Products.Include(y => y.ProductCupons).FirstOrDefault(x => x.Id == addCuponForProductDTO.ProductId);
            if (checekdProduct is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdProduct.ProductCupons.FirstOrDefault(x => x.Cupon.DisCountPercent == addCuponForProductDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Cupon.Code, HttpStatusCode.AlreadyReported);


            Cupon cupon = new Cupon()
            {
                DisCountPercent = addCuponForProductDTO.DisCountPercent,
                Code = GenerateCouponCode.GenerateCouponCodeFromGuid(),
            };
            _appDBContext.Cupons.Add(cupon);
            ProductCupon ProductCupon = new ProductCupon()
            {
                ProductId = checekdProduct.Id,
                CuponId = cupon.Id,
                IsActive = true,
            };
            _appDBContext.ProductCupons.Add(ProductCupon);
            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);
        }

        public IDataResult<string> AddSpecificCuponForSubCategory(AddCuponForSubCategoryDTO addCuponForSubCategoryDTO)
        {
            var checekdSubCategory = _appDBContext.SubCategories.Include(y => y.SubCategoryCupons).FirstOrDefault(x => x.Id == addCuponForSubCategoryDTO.SubCategoryId);
            if (checekdSubCategory is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdSubCategory.SubCategoryCupons.FirstOrDefault(x => x.Cupon.DisCountPercent == addCuponForSubCategoryDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Cupon.Code, HttpStatusCode.AlreadyReported);


            Cupon cupon = new Cupon()
            {
                DisCountPercent = addCuponForSubCategoryDTO.DisCountPercent,
                Code = GenerateCouponCode.GenerateCouponCodeFromGuid(),
            };
            _appDBContext.Cupons.Add(cupon);
            SubCategoryCupon SubCategoryCupon = new SubCategoryCupon()
            {
                SubCategoryId = checekdSubCategory.Id,
                CuponId = cupon.Id,
                IsActive = true,

            };
            _appDBContext.SubCategoryCupons.Add(SubCategoryCupon);
            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);
        }

        public IDataResult<string> AddSpecificCuponForUser(AddCuponForUserDTO addedCuponForUserDTO)
        {
            var checekdUser = _appDBContext.Users.Include(y => y.Cupons).FirstOrDefault(x => x.Id == addedCuponForUserDTO.UserId);
            if (checekdUser is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdUser.Cupons.FirstOrDefault(x => x.Cupon.DisCountPercent == addedCuponForUserDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Cupon.Code, HttpStatusCode.AlreadyReported);


            Cupon cupon = new Cupon()
            {
                DisCountPercent = addedCuponForUserDTO.DisCountPercent,
                Code = GenerateCouponCode.GenerateCouponCodeFromGuid(),
            };
            _appDBContext.Cupons.Add(cupon);
            UserCupon UserCupon = new UserCupon()
            {
                UserId = checekdUser.Id,
                CuponId = cupon.Id,
                isActive = true,
            };
            _appDBContext.UserCupons.Add(UserCupon);
            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);

        }

        public IResult ChangeStatusCupon(UpdateStatusCuponDTO updateStatusCuponDTO)
        {
            var checkedCupon = _appDBContext.Cupons.FirstOrDefault(x => x.Id == updateStatusCuponDTO.CuponId);
            if (checkedCupon is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            if (updateStatusCuponDTO.CategoryId != Guid.Empty)
            {
                var checkedCategoryCupon = _appDBContext.CategoryCupons.FirstOrDefault(x => x.CuponId == updateStatusCuponDTO. CuponId && x.CategoryId == updateStatusCuponDTO. CategoryId);
                if (checkedCategoryCupon is not null)
                {

                    checkedCategoryCupon.IsActive = updateStatusCuponDTO.isActive;
                    _appDBContext.CategoryCupons.Update(checkedCategoryCupon);
                }
            }
            if (updateStatusCuponDTO.SubCategoryId != Guid.Empty)
            {
                var checkedSubCategoryCupon = _appDBContext.SubCategoryCupons.FirstOrDefault(x => x.CuponId == updateStatusCuponDTO.CuponId && x.SubCategoryId == updateStatusCuponDTO.SubCategoryId);
                if (checkedSubCategoryCupon is not null)
                {

                    checkedSubCategoryCupon.IsActive = updateStatusCuponDTO.isActive;
                    _appDBContext.SubCategoryCupons.Update(checkedSubCategoryCupon);
                }
            }
            if (updateStatusCuponDTO.UserId != Guid.Empty)
            {
                var checkedUserCupon = _appDBContext.UserCupons.FirstOrDefault(x => x.CuponId == updateStatusCuponDTO.CuponId && x.UserId == updateStatusCuponDTO.UserId);
                if (checkedUserCupon is not null)
                {

                    checkedUserCupon.isActive = updateStatusCuponDTO.isActive;
                    _appDBContext.UserCupons.Update(checkedUserCupon);
                }
            }
            if (updateStatusCuponDTO.ProductId != Guid.Empty)
            {
                var checkedProdcutCupon = _appDBContext.ProductCupons.FirstOrDefault(x => x.CuponId == updateStatusCuponDTO.CuponId && x.ProductId == updateStatusCuponDTO.ProductId);
                if (checkedProdcutCupon is not null)
                {
                    checkedProdcutCupon.IsActive = updateStatusCuponDTO.isActive;
                    _appDBContext.ProductCupons.Update(checkedProdcutCupon);
                }
            }
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);

        }

        public IDataResult<GetCuponInfoDTO> CheckedCuponCode(string cuponCode)
        {
            var checkedCuponCope = _appDBContext.Cupons.AsSplitQuery().AsNoTracking().FirstOrDefault(x => x.Code == cuponCode);
            if (checkedCuponCope is null)
                return new ErrorDataResult<GetCuponInfoDTO>(HttpStatusCode.NotFound);
          
            return new SuccessDataResult<GetCuponInfoDTO>(response: new GetCuponInfoDTO
            { 
                CuponId=checkedCuponCope.Id,
                CuponCode=checkedCuponCope.Code,
                CategoriesId=checkedCuponCope.CategoryCupons is not null ?checkedCuponCope.CategoryCupons.Select(x=>x.Category.Id):null,
              UserId= checkedCuponCope.UserCupons is not null? checkedCuponCope.UserCupons.Select(x=>x.UserId):null,
              ProductIDs=checkedCuponCope.ProductCupons is not null ?checkedCuponCope.ProductCupons.Select(x=>x.ProductId):null,
              SubCategories=checkedCuponCope.SubCategoryCupons is not null?checkedCuponCope.SubCategoryCupons.Select(x=>x.SubCategoryId):null,
              DisCountPercent=checkedCuponCope.DisCountPercent
                
            }, HttpStatusCode.OK);
        }

        public IResult RemoveCupon(Guid Id)
        {
            var checkedCupon = _appDBContext.Cupons.FirstOrDefault(x => x.Id == Id);
            if (checkedCupon is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            var relatedCategoryCupons = _appDBContext.CategoryCupons
      .Where(cc => cc.CuponId == Id);
            _appDBContext.CategoryCupons.RemoveRange(relatedCategoryCupons);
            _appDBContext.Cupons.Remove(checkedCupon);
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult UpdateCupon(UpdateCuponDTO updateCuponDTO)
        {
            var checkedCupon = _appDBContext.Cupons.AsSplitQuery().FirstOrDefault(x => x.Id == updateCuponDTO.CuponId);

            if (checkedCupon is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            if (checkedCupon.DisCountPercent != updateCuponDTO.DisCountPercent)
            {

                checkedCupon.DisCountPercent = updateCuponDTO.DisCountPercent;
                _appDBContext.Cupons.Update(checkedCupon);
            }
            if (updateCuponDTO.CategoryId != Guid.Empty)
            {
                var checkedCategoryCupon = checkedCupon.CategoryCupons?.FirstOrDefault(x => x.CuponId == updateCuponDTO.CuponId && x.CategoryId == updateCuponDTO.CategoryId);
                if (checkedCategoryCupon is null)
                {
                    var checkedCategory = _appDBContext.Categories.AsNoTracking().FirstOrDefault(x => x.Id == updateCuponDTO.CategoryId);
                    if (checkedCategory is null) return new ErrorResult(HttpStatusCode.NotFound);
                    CategoryCupon categoryCupon = new CategoryCupon()
                    {
                        CategoryId = checkedCategory.Id,
                        CuponId = checkedCupon.Id,
                    };
                    _appDBContext.CategoryCupons.Add(categoryCupon);
                }
            }
            if (updateCuponDTO.SubCategoryId != Guid.Empty)
            {
                var checkedSubCategoryCupon = checkedCupon.SubCategoryCupons?.FirstOrDefault(x => x.CuponId == updateCuponDTO.CuponId && x.SubCategoryId == updateCuponDTO.SubCategoryId);
                if (checkedSubCategoryCupon is null)
                {
                    var checkedSubCategory = _appDBContext.SubCategories.FirstOrDefault(x => x.Id == updateCuponDTO.SubCategoryId);
                    if (checkedSubCategory is null) return new ErrorResult(HttpStatusCode.NotFound);
                    SubCategoryCupon SubCategoryCupon = new SubCategoryCupon()
                    {
                        SubCategoryId = checkedSubCategory.Id,
                        CuponId = checkedCupon.Id,
                    };
                    _appDBContext.SubCategoryCupons.Add(SubCategoryCupon);
                }
            }
            if (updateCuponDTO.UserId != Guid.Empty)
            {
                var checkedUserCupon = checkedCupon.UserCupons.FirstOrDefault(x => x.CuponId == updateCuponDTO.CuponId && x.UserId == updateCuponDTO.UserId);
                if (checkedUserCupon is  null)
                {
                    var checkedUser =_appDBContext.Users.FirstOrDefault(x => x.Id == updateCuponDTO.UserId);
                    if (checkedUser is null) return new ErrorResult(HttpStatusCode.NotFound);
                   UserCupon userCupon = new UserCupon()
                    {
                        UserId = checkedUser.Id,
                        CuponId = checkedCupon.Id,
                    };
                    _appDBContext.UserCupons.Add(userCupon);
                }
            }
            if (updateCuponDTO.ProductId != Guid.Empty)
            {
             
                    var checkedProductCupon = checkedCupon.ProductCupons.FirstOrDefault(x => x.CuponId == updateCuponDTO.CuponId && x.ProductId == updateCuponDTO.ProductId);
                    if (checkedProductCupon is null)
                    {
                        var checkedProduct = _appDBContext.Products.AsNoTracking().FirstOrDefault(x => x.Id == updateCuponDTO.UserId);
                        if (checkedProduct is null) return new ErrorResult(HttpStatusCode.NotFound);
                       ProductCupon productCupon = new ProductCupon()
                        {
                            ProductId = checkedProduct.Id,
                            CuponId = checkedCupon.Id,
                        };
                        _appDBContext.ProductCupons.Add(productCupon);
                    }
               
            }
           
            
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
