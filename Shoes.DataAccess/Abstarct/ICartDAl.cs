using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.CartDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface ICartDAl
    {
        public IResult CheckCartData(GetCartDataDTO getCartDataDTO);
    }
}
