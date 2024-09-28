namespace Shoes.Entites
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; }

        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
        public decimal YeastCost { get; set; }

        public List<Picture> Pictures { get; set; }
        public List<SizeProduct> SizeProducts { get; set; }
        public List<SubCategoryProduct> SubCategories { get; set; }
        public List<ProductLanguage> ProductLanguages { get; set; }
    }
}
