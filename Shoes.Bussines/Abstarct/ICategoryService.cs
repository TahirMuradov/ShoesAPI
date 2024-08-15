using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Bussines.Abstarct
{
    public interface ICategoryService
    {
        public IResult AddCategory(AddCategoryDTO categoryDTO , string langCode);
        public IResult DeleteCategory(Guid Id, string langCode);
        public IResult UpdateCategory(UpdateCategoryDTO updateCategory, string langCode);
        public IDataResult<GetCategoryDTO> GetCategory(Guid Id, string LangCode);
        public Task<IDataResult<PaginatedList<GetCategoryDTO>>> GetAllCategoryAsync(string LangCode, int page = 1);
    }
}
