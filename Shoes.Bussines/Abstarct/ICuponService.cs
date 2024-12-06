using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CuponDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface ICuponService
    {
        public IDataResult<string> AddSpecificCuponForUser(AddCuponForUserDTO addedCuponForUserDTO,string LangCode);
        public IDataResult<string> AddSpecificCuponForCategory(AddCuponForCategoryDTO addCuponForCategoryDTO,string LangCode);
        public IDataResult<string> AddSpecificCuponForSubCategory(AddCuponForSubCategoryDTO addCuponForSubCategoryDTO, string LangCode);
        public IDataResult<string> AddSpecificCuponForProduct(AddCuponForProductDTO addCuponForProductDTO,string LangCode);
        public IDataResult<GetCuponDetailDTO> GetCuponDetail(Guid CuponId, string LangCode);
        public Task<IDataResult<PaginatedList<GetAllCuponDTO>>> GetAllCuponAsync(int page, string LangCode);
        public IResult RemoveCupon(Guid Id);
        public IResult ChangeStatusCupon(UpdateStatusCuponDTO updateStatusCuponDTO,string LangCode);
        public IResult UpdateCupon(UpdateCuponDTO updateCuponDTO,string LangCode);
        public IDataResult<GetCuponInfoDTO> CheckedCuponCode(string cuponCode);


    }
}
