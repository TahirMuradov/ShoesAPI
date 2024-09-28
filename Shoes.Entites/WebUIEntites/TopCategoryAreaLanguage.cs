namespace Shoes.Entites.WebUIEntites
{
    public class TopCategoryAreaLanguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid TopCategoryAreaId { get; set; }
        public TopCategoryArea TopCategoryArea { get; set; }
    }
}
