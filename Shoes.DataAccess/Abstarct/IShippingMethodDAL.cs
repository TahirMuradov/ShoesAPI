using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.ShippingMethodDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface IShippingMethodDAL
    {
        public IResult AddShippingMethod(AddShippingMethodDTO addShipping);
        public IDataResult<GetShippingMethodForUpdateDTO> GetShippingMethodForUpdate(Guid Id);
        public IResult UpdateShippingMethod(UpdateShippingMethodDTO updateShipping);
        public IResult DeleteShippingMethod(Guid Id);
        public IDataResult<GetShippingMethodDTO> GetShippingMethod(Guid Id, string LangCode);
        public Task<IDataResult<PaginatedList<GetShippingMethodDTO>>> GetAllShippingMethodAsync(string LangCode,int page=1);
    }
}
