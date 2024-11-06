using Shoes.Bussines.Abstarct;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.CartDTOs;

namespace Shoes.Bussines.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDAl _cartDAl;

        public CartManager(ICartDAl cartDAl)
        {
            _cartDAl = cartDAl;
        }

        public IResult CheckCartData(GetCartDataDTO getCartDataDTO)
        {
            return _cartDAl.CheckCartData(getCartDataDTO);
        }
    }
}
