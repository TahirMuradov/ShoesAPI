namespace Shoes.Entites
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public List<SubCategoryLanguage> SubCategoryLanguages { get; set; }
        public List<SubCategoryProduct> SubCategoryProducts { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
