namespace Shoes.Entites
{
    public class ShippingMethod
    {
        public Guid Id { get; set; }
        public List<Order> Orders { get; set; }
        public decimal discountPrice { get; set; }
        public decimal price { get; set; }
    }
}
