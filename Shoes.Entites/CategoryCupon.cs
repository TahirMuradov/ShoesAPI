namespace Shoes.Entites
{
    public class CategoryCupon
    {
        public Guid Id { get; set; }
        public Guid CuponId { get; set; }
        public Cupon Cupon { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsActive { get; set; }
    }
}
