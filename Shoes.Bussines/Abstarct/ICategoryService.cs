using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CategoryDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface ICategoryService
    {
        
        public IResult AddCategory(AddCategoryDTO categoryDTO , string langCode);
        public IResult DeleteCategory(Guid Id, string langCode);
        public IResult UpdateCategory(UpdateCategoryDTO updateCategory, string langCode);
        public IDataResult<GETCategoryForUpdateDTO> GetCategoryForUpdate(Guid Id);
        public IDataResult<GetCategoryDTO> GetCategory(Guid Id, string LangCode);
        public IDataResult<IQueryable<GetAllCategoryForSelectDTO>> GetAllCategoryForSelect(string LangCode);
        public IDataResult<IQueryable<GetCategoryForUIDTO>> GetAllCategory(string LangCode);
        public Task<IDataResult<PaginatedList<GetCategoryDTO>>> GetAllCategoryAsync(string LangCode, int page = 1);
    }
}
