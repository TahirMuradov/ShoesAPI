namespace Shoes.Entites.DTOs.ShippingMethodDTOs
{
    public class UpdateShippingMethodDTO
    {
        public Guid Id { get; set; }
        public decimal discountPrice { get; set; }
        public decimal price { get; set; }
        public Dictionary<string, string> Lang { get; set; }
    }
}
