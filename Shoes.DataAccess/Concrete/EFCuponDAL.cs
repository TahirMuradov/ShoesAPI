using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.CuponDTOs;
using System;
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
            var checekdCategory = _appDBContext.Categories.Include(y=>y.CategoryCupons).FirstOrDefault(x => x.Id == addCuponForCategoryDTO.CategoryId);
            if (checekdCategory is null)  return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdCategory.CategoryCupons.FirstOrDefault(x => x.Cupon.DisCountPercent == addCuponForCategoryDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Cupon.Code, HttpStatusCode.AlreadyReported);

            Random random = new Random();
            Cupon cupon = new Cupon()
            {
                DisCountPercent=addCuponForCategoryDTO.DisCountPercent,
                Code=GenerateCouponCode.GenerateCouponCodeFromGuid(),              
        };
            _appDBContext.Cupons.Add(cupon);
            CategoryCupon categoryCupon = new CategoryCupon()
            {
                CategoryId=checekdCategory.Id,
                CuponId=cupon.Id,
                IsActive=true,
            };
            _appDBContext.CategoryCupons.Add(categoryCupon);
            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response:cupon.Code, HttpStatusCode.OK);

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
    }
}
