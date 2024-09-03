using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.SubCategoryDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFSubCategoryDAL:ISubCategoryDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFSubCategoryDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IResult AddSubCategory(AddSubCategoryDTO addCategory)
        {
            try
            {
                Category checekdCategoryId = _appDBContext.Categories.FirstOrDefault(x => x.Id == addCategory.CategoryId);
                if (checekdCategoryId is null)
                    return new ErrorResult(HttpStatusCode.NotFound);
                SubCategory subCategory = new SubCategory()
                {
                    CategoryId = checekdCategoryId.Id,

                };
                _appDBContext.SubCategories.Add(subCategory);
                foreach (var lang in addCategory.LangContent)
                {
                    SubCategoryLanguage subCategoryLanguage = new()
                    {
                        LangCode=lang.Key,
                        Content=lang.Value,
                        SubCategoryId=subCategory.Id
                    };
                    _appDBContext.SubCategoryLanguages.Add(subCategoryLanguage);
                }
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteSubCategory(Guid Id)
        {
            try
            {
                SubCategory subCategory = _appDBContext.SubCategories.FirstOrDefault(x => x.Id == Id);
                if (subCategory is null)
                    return new ErrorResult(HttpStatusCode.NotFound);
                _appDBContext.Remove(subCategory);
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<PaginatedList<GetSubCategoryDTO>>> GetAllSubCategoryAsync(string LangCode, int page = 1)
        {
            try
            {
                IQueryable<GetSubCategoryDTO> SubcategoryQuery = _appDBContext.SubCategories.AsNoTracking().AsSplitQuery()
                    .Select(x => new GetSubCategoryDTO
                    {
                        Id = x.Id,
                        Content = x.SubCategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode).Content
                    });
                var returnData = await PaginatedList<GetSubCategoryDTO>.CreateAsync(SubcategoryQuery, page, 10);
                return new SuccessDataResult<PaginatedList<GetSubCategoryDTO>>(response: returnData, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<PaginatedList<GetSubCategoryDTO>>(message:ex.Message,statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<GetSubCategoryDTO> GetSubCategory(Guid Id, string LangCode)
        {
            try
            {
               var subCategory = _appDBContext.SubCategories
                                 .Include(x => x.SubCategoryLanguages)               
                    .FirstOrDefault(x => x.Id == Id)      
                    ;
                if (subCategory is null)              
                return new ErrorDataResult<GetSubCategoryDTO>(HttpStatusCode.NotFound);
                return new SuccessDataResult<GetSubCategoryDTO>(response:new GetSubCategoryDTO
                {
                    Id = subCategory.Id,
                    Content = subCategory.SubCategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content
               
                    
                }, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetSubCategoryDTO>(message: ex.Message, statusCode: HttpStatusCode.OK);
            }
        }

        public IDataResult<GetSubCategoryForUpdateDTO> GetSubCategoryForUpdate(Guid Id)
        {
            try
            {
                var subCategory = _appDBContext.SubCategories
                                  .Include(x => x.SubCategoryLanguages)
                     .FirstOrDefault(x => x.Id == Id)
                     ;
                if (subCategory is null)
                    return new ErrorDataResult<GetSubCategoryForUpdateDTO>(HttpStatusCode.NotFound);
                return new SuccessDataResult<GetSubCategoryForUpdateDTO>(response: new GetSubCategoryForUpdateDTO
                {
                    Id = subCategory.Id,
                    CategoryId = subCategory.CategoryId,
                    Content = subCategory.SubCategoryLanguages.Select(x=> new KeyValuePair<string,string>(x.LangCode,x.Content)).ToDictionary()


                }, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetSubCategoryForUpdateDTO>(message: ex.Message, statusCode: HttpStatusCode.OK);
            }
        }

        public IResult UpdateSubCategory(UpdateSubCategoryDTO updateCategory)
        {
            try
            {
                var data = _appDBContext.SubCategories.Include(x => x.SubCategoryLanguages).FirstOrDefault(x => x.Id == updateCategory.Id);
                if (data is null)
              return new ErrorResult(HttpStatusCode.NotFound);
                if (data.CategoryId!=updateCategory.CategoryId)
                {
                    data.CategoryId = updateCategory.CategoryId;
                    _appDBContext.SubCategories.Update(data);
                }
                foreach (var lang in updateCategory.LangContent)
                {
                    var subCategoryLang = data.SubCategoryLanguages.FirstOrDefault(x => x.LangCode == lang.Key);
                    if (subCategoryLang is null)
                    {
                        SubCategoryLanguage NewLanguage = new SubCategoryLanguage()
                        {
                            LangCode = lang.Key,
                            Content = lang.Value,
                            SubCategoryId=data.Id,
                            
                        };
                        _appDBContext.SubCategoryLanguages.Add(NewLanguage);
                    }else
                    {

                    subCategoryLang.Content = lang.Value;
                    _appDBContext.SubCategoryLanguages.Update(subCategoryLang);
                    }
               
                }
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorResult(message:ex.Message, statusCode: HttpStatusCode.BadRequest);

            }
        }
    }
}
