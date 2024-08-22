namespace Shoes.Entites
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string OrderNumber { get; set; }
        public string? Message { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public string OrderPDfUrl { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<SoldProduct> SoldProducts { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
