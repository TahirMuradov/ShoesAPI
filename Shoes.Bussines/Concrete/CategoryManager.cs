using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.CategoryDTOValidations;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites;
using Shoes.Entites.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            if (!result.IsValid)
                return new ErrorResult(statusCode:HttpStatusCode.UnsupportedMediaType);
            return _categoryDAL.AddCategory(categoryDTO);
        }

        public IResult DeleteCategory(Guid Id , string langCode)
        {
           return _categoryDAL.DeleteCategory(Id);
        }

        public IDataResult<List<GetCategoryDTO>> GetAllCategory(string LangCode)
        {
            throw new NotImplementedException();
        }

        public IDataResult<GetCategoryDTO> GetCategory(string Id, string LangCode)
        {
            throw new NotImplementedException();
        }

        public IResult UpdateCategory(UpdateCategoryDTO updateCategory , string langCode)
        {
            throw new NotImplementedException();
        }
    }
}
