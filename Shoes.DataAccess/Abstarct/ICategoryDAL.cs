﻿using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CategoryDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface ICategoryDAL
    {
        public IResult AddCategory(AddCategoryDTO categoryDTO);
        public IResult DeleteCategory(Guid Id);
        public IResult UpdateCategory(UpdateCategoryDTO updateCategory);
        public IDataResult<GetCategoryDTO> GetCategory(Guid Id, string LangCode);
        public IDataResult<GETCategoryForUpdateDTO> GetCategoryForUpdate(Guid Id);
        public Task<IDataResult<PaginatedList<GetCategoryDTO>>> GetAllCategoryAsync(string LangCode,int page=1);

    }
}
