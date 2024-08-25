using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.ShippingMethodDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IShippingMethodService
    {
        public IResult AddShippingMethod(AddShippingMethodDTO addShipping,string LangCode);
        public IResult UpdateShippingMethod(UpdateShippingMethodDTO updateShipping,string LangCode);
        public IResult DeleteShippingMethod(Guid Id);
        public IDataResult<GetShippingMethodDTO> GetShippingMethod(Guid Id, string LangCode);
        public Task<IDataResult<PaginatedList<GetShippingMethodDTO>>> GetAllShippingMethodAsync(string LangCode, int page = 1);

    }
}
