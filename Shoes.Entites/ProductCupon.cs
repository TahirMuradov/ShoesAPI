namespace Shoes.Entites
{
    public class ProductCupon
    {
        public Guid Id { get; set; }
        public Guid CuponId { get; set; }
        public Cupon   Cupon { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; }
    }
}
