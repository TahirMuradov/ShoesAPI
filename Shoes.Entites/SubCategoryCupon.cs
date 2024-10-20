namespace Shoes.Entites
{
    public class SubCategoryCupon
    {
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public Guid CuponId { get; set; }
        public Cupon Cupon { get; set; }
    }
}
