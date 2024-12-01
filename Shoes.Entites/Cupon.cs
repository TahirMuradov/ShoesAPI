namespace Shoes.Entites
{
    public class Cupon
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal DisCountPercent { get; set; }
        public bool IsActive { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid?  SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public Guid?  UserId { get; set; }
        public User? User { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
