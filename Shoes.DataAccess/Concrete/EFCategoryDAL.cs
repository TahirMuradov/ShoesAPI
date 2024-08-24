using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.PageHelper;
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
                foreach (var lang in categoryDTO.LangContent)
                {
                    CategoryLanguage categoryLanguage = new()
                    {
                        CategoryId = category.Id,
                        Content = lang.Value,
                        LangCode = lang.Key
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

        public async Task<IDataResult<PaginatedList<GetCategoryDTO>>> GetAllCategoryAsync(string LangCode,int page=1)
        {
            IQueryable<GetCategoryDTO> categoryQuery = _dBContext.Categories.AsNoTracking().AsSplitQuery()
                .Select(x=>new GetCategoryDTO
                {
                    Id = x.Id,
                    Content=x.CategoryLanguages.FirstOrDefault(x=>x.LangCode==LangCode).Content
                });
          var returnData= await PaginatedList<GetCategoryDTO>.CreateAsync(categoryQuery, page, 10);
            return new SuccessDataResult<PaginatedList<GetCategoryDTO>>(response:returnData,HttpStatusCode.OK);



        }

        public IDataResult<GetCategoryDTO> GetCategory(Guid Id, string LangCode)
        {
           Category categoryQuery = _dBContext.Categories.AsNoTracking().Include(x=>x.CategoryLanguages).FirstOrDefault(x=>x.Id==Id);
            
            if (categoryQuery is null)
                return new ErrorDataResult<GetCategoryDTO>(statusCode: HttpStatusCode.NotFound);
            return new SuccessDataResult<GetCategoryDTO>(response: new GetCategoryDTO
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
