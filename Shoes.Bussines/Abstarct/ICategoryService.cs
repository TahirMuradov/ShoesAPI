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
        public IResult AddCategory(AddCategoryDTO categoryDTO,string langCode);
        public IResult DeleteCategory(Guid Id, string langCode);
        public IResult UpdateCategory(UpdateCategoryDTO updateCategory, string langCode);
        public IDataResult<GetCategoryDTO> GetCategory(string Id, string LangCode);
        public IDataResult<List<GetCategoryDTO>> GetAllCategory(string LangCode);
    }
}
