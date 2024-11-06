using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CartDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface ICartService
    {
        public IResult CheckCartData(GetCartDataDTO getCartDataDTO);
    }
}
