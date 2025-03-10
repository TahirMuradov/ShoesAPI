﻿using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CuponDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface ICuponDAL
    {
        public IDataResult<string> AddSpecificCuponForUser(AddCuponForUserDTO addedCuponForUserDTO);
        public IDataResult<string> AddSpecificCuponForCategory(AddCuponForCategoryDTO addCuponForCategoryDTO);
        public IDataResult<string> AddSpecificCuponForSubCategory(AddCuponForSubCategoryDTO addCuponForSubCategoryDTO );
        public IDataResult<string> AddSpecificCuponForProduct(AddCuponForProductDTO addCuponForProductDTO);
        public IResult RemoveCupon(Guid Id);
        public IResult ChangeStatusCupon(UpdateStatusCuponDTO updateStatusCuponDTO);
        public IResult UpdateCupon(UpdateCuponDTO updateCuponDTO);
        public IDataResult<GetCuponInfoDTO> CheckedCuponCode(string cuponCode);
        public Task<IDataResult<PaginatedList<GetAllCuponDTO>>> GetAllCuponAsync(int page,string LangCode);
        public IDataResult<GetCuponDetailDTO> GetCuponDetail(Guid CuponId,string LangCode);


    }
}
