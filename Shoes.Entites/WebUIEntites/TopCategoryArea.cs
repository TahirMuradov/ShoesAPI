namespace Shoes.Entites.WebUIEntites
{
    public class TopCategoryArea
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public List<TopCategoryAreaLanguage> TopCategoryAreaLanguages { get; set; }

    }
}
