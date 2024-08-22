namespace Shoes.Entites
{
    public class SizeProduct
    {
        public Guid Id { get; set; }
        public int  StockCount { get; set; }
        public Guid SizeId { get; set; }
        public Size Size { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
