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
        public IDataResult<decimal> CheckedCuponCode(string cuponCode);       


    }
}
