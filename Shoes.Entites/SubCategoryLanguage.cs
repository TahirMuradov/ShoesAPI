namespace Shoes.Entites
{
    public class SubCategoryLanguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string Content { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
