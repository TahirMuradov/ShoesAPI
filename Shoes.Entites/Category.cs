namespace Shoes.Entites
{
    public class Category
    {
        public Guid Id { get; set; }
        public bool IsFeatured { get; set; }
        public List<CategoryLanguage> CategoryLanguages { get; set; }
        public List<SubCategory>? SubCategories { get; set; }

    }
}
