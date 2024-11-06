namespace Shoes.Entites.DTOs.CartDTOs
{
    public class GetCartDataDTO
    {
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public ShippingMethodCartDTO? ShippingMethod { get; set; }
        public IEnumerable<CartItemDTO> cartItems { get; set; }
    }
}
