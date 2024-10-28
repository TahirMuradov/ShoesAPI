﻿using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CuponDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface ICuponDAL
    {
        public IDataResult<string> AddSpecificCuponForUser(AddCuponForUserDTO addedCuponForUserDTO);
        public IDataResult<string> AddSpecificCuponForCategory(AddCuponForCategoryDTO addCuponForCategoryDTO);
        public IDataResult<string> AddSpecificCuponForSubCategory(AddCuponForSubCategoryDTO addCuponForSubCategoryDTO );
        public IDataResult<string> AddSpecificCuponForProduct(AddCuponForProductDTO addCuponForProductDTO);

    }
}
