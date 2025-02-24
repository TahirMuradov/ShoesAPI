﻿using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
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
            var checekdCategory = _appDBContext.Categories.Include(y => y.Cupons).AsNoTracking().FirstOrDefault(x => x.Id == addCuponForCategoryDTO.CategoryId);
            if (checekdCategory is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdCategory.Cupons.FirstOrDefault(x => x.DisCountPercent == addCuponForCategoryDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Code, HttpStatusCode.AlreadyReported);
            codeGenerate: string CuponCode = GenerateCouponCode.GenerateCouponCodeFromGuid();
            if (_appDBContext.Cupons.AsNoTracking().Any(x => x.Code == CuponCode))
                goto codeGenerate;

            Cupon cupon = new Cupon()
            {
                DisCountPercent = addCuponForCategoryDTO.DisCountPercent,
                Code = CuponCode,
                IsActive = true,
                CategoryId = checekdCategory.Id,
                  ProductId = null,
                SubCategoryId = null,
                UserId = null,
            };
            _appDBContext.Cupons.Add(cupon);


            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);

        }

        public IDataResult<string> AddSpecificCuponForProduct(AddCuponForProductDTO addCuponForProductDTO)
        {
            var checekdProduct = _appDBContext.Products.Include(y => y.Cupons).AsNoTracking().FirstOrDefault(x => x.Id == addCuponForProductDTO.ProductId);
            if (checekdProduct is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdProduct.Cupons.FirstOrDefault(x => x.DisCountPercent == addCuponForProductDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Code, HttpStatusCode.AlreadyReported);


            codeGenerate: string CuponCode = GenerateCouponCode.GenerateCouponCodeFromGuid();
            if (_appDBContext.Cupons.AsNoTracking().Any(x => x.Code == CuponCode))
                goto codeGenerate;

            Cupon cupon = new Cupon()
            {
                DisCountPercent = addCuponForProductDTO.DisCountPercent,
                Code = CuponCode,
                IsActive = true,
                CategoryId = null,
                ProductId = checekdProduct.Id,
                SubCategoryId = null,
                UserId = null,
            };
            _appDBContext.Cupons.Add(cupon);


            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);


        }

        public IDataResult<string> AddSpecificCuponForSubCategory(AddCuponForSubCategoryDTO addCuponForSubCategoryDTO)
        {
            var checekdSubCategory = _appDBContext.SubCategories.Include(y => y.Cupons).FirstOrDefault(x => x.Id == addCuponForSubCategoryDTO.SubCategoryId);
            if (checekdSubCategory is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdSubCategory.Cupons.FirstOrDefault(x => x.DisCountPercent == addCuponForSubCategoryDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Code, HttpStatusCode.AlreadyReported);

            codeGenerate: string CuponCode = GenerateCouponCode.GenerateCouponCodeFromGuid();
            if (_appDBContext.Cupons.AsNoTracking().Any(x => x.Code == CuponCode))
                goto codeGenerate;

            Cupon cupon = new Cupon()
            {
                DisCountPercent = addCuponForSubCategoryDTO.DisCountPercent,
                Code = CuponCode,
                IsActive = true,
                CategoryId = null,
                ProductId = null,
                SubCategoryId = checekdSubCategory.Id,
                UserId = null,
            };
            _appDBContext.Cupons.Add(cupon);


            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);
        }

        public IDataResult<string>AddSpecificCuponForUser(AddCuponForUserDTO addedCuponForUserDTO)
        {
            var checekdUser =  _appDBContext.Users.Include(x=>x.Cupons).AsNoTracking().FirstOrDefault ( x=>x.Id==addedCuponForUserDTO.UserId);
            if (checekdUser is null) return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            var checekdPercent = checekdUser.Cupons.FirstOrDefault(x => x.DisCountPercent == addedCuponForUserDTO.DisCountPercent);
            if (checekdPercent is not null) return new ErrorDataResult<string>(response: checekdPercent.Code, HttpStatusCode.AlreadyReported);
            codeGenerate: string CuponCode = GenerateCouponCode.GenerateCouponCodeFromGuid();
            if (_appDBContext.Cupons.AsNoTracking().Any(x => x.Code == CuponCode))
                goto codeGenerate;

            Cupon cupon = new Cupon()
            {
                DisCountPercent = addedCuponForUserDTO.DisCountPercent,
                Code = CuponCode,
                IsActive = true,
                CategoryId = null,
                ProductId = null,
                SubCategoryId = null,
                UserId = checekdUser.Id,
            };
            _appDBContext.Cupons.Add(cupon);
            _appDBContext.SaveChanges();
            return new SuccessDataResult<string>(response: cupon.Code, HttpStatusCode.OK);

        }

        public IResult ChangeStatusCupon(UpdateStatusCuponDTO updateStatusCuponDTO)
        {
            var checkedCupon = _appDBContext.Cupons.FirstOrDefault(x => x.Id == updateStatusCuponDTO.CuponId);
            if (checkedCupon is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            checkedCupon.IsActive = !checkedCupon.IsActive;
            _appDBContext.Cupons.Update(checkedCupon);
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
                CuponId = checkedCuponCope.Id,
                CuponCode = checkedCuponCope.Code,
                CategoryId = checkedCuponCope.CategoryId??null,
                UserId = checkedCuponCope.UserId??null,
                ProductID = checkedCuponCope.ProductId??null,
                SubCategoryId = checkedCuponCope.SubCategoryId ?? null,
                DisCountPercent = checkedCuponCope.DisCountPercent

            }, HttpStatusCode.OK);
        }

        public async Task<IDataResult<PaginatedList<GetAllCuponDTO>>> GetAllCuponAsync(int page,string LangCode)
        {
            var query = _appDBContext.Cupons.AsSplitQuery().AsNoTracking().Select(x => new GetAllCuponDTO
            {
                CuponId = x.Id,
                CuponCode = x.Code,
                IsActive = x.IsActive,
                DisCountPercent = x.DisCountPercent,
                ProductCode = x.Product.ProductCode,
                CategoryName = x.Category.CategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content,
                SubCategoryName = x.SubCategory.SubCategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content,
                UserEmail = x.User.Email,

            });
            var resultData = await PaginatedList<GetAllCuponDTO>.CreateAsync(query,page , 10);
            return new SuccessDataResult<PaginatedList<GetAllCuponDTO>>(resultData, HttpStatusCode.OK);
        }

        public IDataResult<GetCuponDetailDTO> GetCuponDetail(Guid CuponId,string LangCode)
        {
            var query = _appDBContext.Cupons.AsSplitQuery().AsNoTracking().Select(x => new GetCuponDetailDTO
            {
                CuponId=x.Id,
                IsActive=x.IsActive,
                CuponCode=x.Code,
                DisCountPercent=x.DisCountPercent,
                Category=new KeyValuePair<Guid, string>(x.Category.Id,x.Category.CategoryLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content),
                SubCategory=new KeyValuePair<Guid, string>(x.SubCategory.Id,x.SubCategory.SubCategoryLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content),
                Product=new KeyValuePair<Guid, string>(x.Product.Id,x.Product.ProductCode),
                User=new KeyValuePair<Guid, string>(x.User.Id,x.User.Email),
                
            }).FirstOrDefault(x => x.CuponId == CuponId);
            if (query is null) return new ErrorDataResult<GetCuponDetailDTO>(HttpStatusCode.NotFound);
            return new SuccessDataResult<GetCuponDetailDTO>(query, HttpStatusCode.OK);
        }

        public IResult RemoveCupon(Guid Id)
        {
            var checkedCupon = _appDBContext.Cupons.FirstOrDefault(x => x.Id == Id);
            if (checkedCupon is null)
                return new ErrorResult(HttpStatusCode.NotFound);          
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
         


            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
