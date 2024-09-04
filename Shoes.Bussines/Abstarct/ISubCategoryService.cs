using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.SubCategoryDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface ISubCategoryService
    {
        public IDataResult<GetSubCategoryForUpdateDTO> GetSubCategoryForUpdate(Guid Id);
        public IDataResult<IQueryable<GetSubCategoryDTO>> GetAllSubCategory(string LangCode);
        public IResult AddSubCategory(AddSubCategoryDTO addCategory,string LangCode);
        public IResult UpdateSubCategory(UpdateSubCategoryDTO updateCategory,string langCode);
        public IResult DeleteSubCategory(Guid Id);
        public IDataResult<GetSubCategoryDTO> GetSubCategory(Guid Id, string LangCode);
        public Task<IDataResult<PaginatedList<GetSubCategoryDTO>>> GetAllSubCategoryForTableAsync(string LangCode, int page = 1);
    }
}
