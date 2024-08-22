namespace Shoes.Entites
{
    public class CategoryLanguage
    {
        public Guid Id { get; set; }
        public string  LangCode { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
