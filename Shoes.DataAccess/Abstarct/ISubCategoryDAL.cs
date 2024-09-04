using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.SubCategoryDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface ISubCategoryDAL
    {
        public IResult AddSubCategory(AddSubCategoryDTO addCategory);
        public IResult UpdateSubCategory(UpdateSubCategoryDTO updateCategory);
        public IResult DeleteSubCategory(Guid Id);
        public IDataResult<GetSubCategoryDTO> GetSubCategory(Guid Id, string LangCode);
        public IDataResult<GetSubCategoryForUpdateDTO> GetSubCategoryForUpdate(Guid Id);
        public Task<IDataResult<PaginatedList<GetSubCategoryDTO>>> GetAllSubCategoryForTableAsync(string LangCode, int page = 1);
        public IDataResult<IQueryable<GetSubCategoryDTO>> GetAllSubCategory(string LangCode);

    }
}
