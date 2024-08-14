namespace Shoes.Entites
{
    public class ShippingMethodLanguage
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string LangCode { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public ShippingMethod DeliveryMethod { get; set; }
    }
}
