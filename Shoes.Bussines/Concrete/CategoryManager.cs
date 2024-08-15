using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.CategoryDTOValidations;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.CategoryDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public IResult AddCategory(AddCategoryDTO categoryDTO,string langCode)
        {
            CategoryAddDTOValidator validationRules = new CategoryAddDTOValidator(langCode);
            var result=validationRules.Validate(categoryDTO);
            if (!result.IsValid||langCode is null)
                return new ErrorResult(messages:result.Errors.Select(x=>x.ErrorMessage).ToList(),statusCode:HttpStatusCode.BadRequest);
            return _categoryDAL.AddCategory(categoryDTO);
        }

   

        public IResult DeleteCategory(Guid Id , string langCode)
        {
            if (Id==default(Guid) || string.IsNullOrEmpty(langCode))
                return new ErrorResult(statusCode: HttpStatusCode.BadRequest);
            return _categoryDAL.DeleteCategory(Id);
        }

    

        public async Task<IDataResult<PaginatedList<GetCategoryDTO>>> GetAllCategoryAsync(string LangCode, int page = 1)

        {
            if (string.IsNullOrEmpty(LangCode) || page <= 0)
                return new ErrorDataResult<PaginatedList<GetCategoryDTO>>(statusCode: HttpStatusCode.BadRequest);

            return await _categoryDAL.GetAllCategoryAsync(LangCode, page);
        }

        public IDataResult<GetCategoryDTO> GetCategory(Guid Id, string LangCode)
        {
            if (Id == default(Guid) || string.IsNullOrEmpty(LangCode))
                return new ErrorDataResult<GetCategoryDTO>(statusCode: HttpStatusCode.BadRequest);
            return _categoryDAL.GetCategory(Id, LangCode);
        }

        public IResult UpdateCategory(UpdateCategoryDTO updateCategory ,string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode))
                return new ErrorResult(HttpStatusCode.BadRequest);
            CategoryUpdateDTOValidator validationRules = new CategoryUpdateDTOValidator(LangCode);
            var result=validationRules.Validate(updateCategory);
            if (!result.IsValid)
                return new ErrorResult(messages: result.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

            return _categoryDAL.UpdateCategory(updateCategory);
        }
    }
}
