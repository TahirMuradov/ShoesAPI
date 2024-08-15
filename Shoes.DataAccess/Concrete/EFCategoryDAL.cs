﻿using Microsoft.EntityFrameworkCore;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.CategoryDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFCategoryDAL : ICategoryDAL
    {
        private readonly AppDBContext _dBContext;

        public EFCategoryDAL(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IResult AddCategory(AddCategoryDTO categoryDTO)
        {
            try
            {
                Category category = new Category();
                _dBContext.Categories.Add(category);
                for (int i = 0; i < categoryDTO.LangCode.Count; i++)
                {
                    CategoryLanguage categoryLanguage= new CategoryLanguage()
                    {
                        LangCode = categoryDTO.LangCode[i],
                        Content = categoryDTO.Content[i],
                        CategoryId=category.Id
                    };
                    _dBContext.CategoryLanguages.Add(categoryLanguage);

                }
                _dBContext.SaveChanges();
              
                return new SuccessResult(statusCode:HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteCategory(Guid Id)
        {
            try
            {
                Category category = _dBContext.Categories.FirstOrDefault(x => x.Id == Id);
               if (category == null) 
                    return new ErrorResult(statusCode: HttpStatusCode.NotFound);
               _dBContext.Categories.Remove(category);
                _dBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<IQueryable<GetCategoryDTO>> GetAllCategory(string LangCode)
        {
            IQueryable<Category> categoryQuery = _dBContext.Categories.AsNoTracking().AsSplitQuery();
            return new SuccessDataResult<IQueryable<GetCategoryDTO>>(data: categoryQuery.Select(x => new GetCategoryDTO
            {
                Id = x.Id,
                Content = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode).Content
            }),
               statusCode:HttpStatusCode.OK );
        }

        public IDataResult<GetCategoryDTO> GetCategory(Guid Id, string LangCode)
        {
           Category categoryQuery = _dBContext.Categories.AsNoTracking().Include(x=>x.CategoryLanguages).FirstOrDefault(x=>x.Id==Id);
            
            if (categoryQuery is null)
                return new ErrorDataResult<GetCategoryDTO>(statusCode: HttpStatusCode.NotFound);
            return new SuccessDataResult<GetCategoryDTO>(data: new GetCategoryDTO
            {
                Id = categoryQuery.Id,
                Content = categoryQuery.CategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode).LangCode
            }, statusCode: HttpStatusCode.OK);
        }

        public IResult UpdateCategory(UpdateCategoryDTO updateCategory)
        {
            try
            {
                Category category=_dBContext.Categories.Include(X=>X.CategoryLanguages).FirstOrDefault(X=>X.Id==updateCategory.Id);
                if (category is null)
                    return new ErrorResult(statusCode: HttpStatusCode.NotFound);
                foreach (var item in updateCategory.Lang)
                {
                    CategoryLanguage categoryLanguage = category.CategoryLanguages.FirstOrDefault(x => x.LangCode == item.Key);
                    if (categoryLanguage is null)
                        continue;
                    categoryLanguage.LangCode = item.Key;
                    categoryLanguage.Content = item.Value;
                    _dBContext.CategoryLanguages.Update(categoryLanguage);

                }
                _dBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

               return new ErrorResult(message:ex.Message,HttpStatusCode.BadRequest);
            }
        }
    }
}
