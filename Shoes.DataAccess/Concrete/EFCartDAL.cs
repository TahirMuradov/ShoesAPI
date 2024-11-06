using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites.DTOs.CartDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFCartDAL : ICartDAl
    {
        private readonly AppDBContext _dBContext;

        public EFCartDAL(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IResult CheckCartData(GetCartDataDTO getCartDataDTO)
        {
            if (getCartDataDTO.ShippingMethod is not null)
            {
                
            var checkedShippingMethod = _dBContext.ShippingMethods.FirstOrDefault(x => x.Id == getCartDataDTO.ShippingMethod.Id);
            if (checkedShippingMethod is null) return new ErrorResult(HttpStatusCode.NotFound);
            if (getCartDataDTO.ShippingMethod.DisCount != checkedShippingMethod.discountPrice || getCartDataDTO.ShippingMethod.Price != checkedShippingMethod.price)
                return new ErrorResult(HttpStatusCode.BadRequest);
            }

            if (getCartDataDTO.cartItems is not null)
            {
                
            foreach (var item in getCartDataDTO.cartItems)
            {
                var checkedCartItem = _dBContext.Products.FirstOrDefault(x => x.Id == item.Id);
                if (checkedCartItem is null) return new ErrorResult(HttpStatusCode.NotFound);
                if(checkedCartItem.SizeProducts.Any(x=>x.Size.SizeNumber==item.Size&&x.StockCount<item.Count))
                    return new ErrorResult(HttpStatusCode.BadRequest);
                if (checkedCartItem.DiscountPrice != 0 && checkedCartItem.DiscountPrice != item.Price)
                    return new ErrorResult(HttpStatusCode.BadRequest);
                else if (checkedCartItem.DiscountPrice == 0 && checkedCartItem.Price != item.Price)
                    return new ErrorResult(HttpStatusCode.BadRequest);  
            }
            }
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
