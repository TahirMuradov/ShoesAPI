namespace Shoes.Entites.DTOs.ShippingMethodDTOs
{
    public class AddShippingMethodDTO
    {
        public decimal discountPrice { get; set; }
        public decimal price { get; set; }
        public Dictionary<string, string> LangContent { get; set; }
    }
}
