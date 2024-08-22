namespace Shoes.Entites
{
    public class SoldProduct
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public Guid SizeId { get; set; }
        public Size Size { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
    }
}
