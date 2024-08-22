namespace Shoes.Entites
{
    public class SubCategoryProduct
    {
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
