namespace Shoes.Entites.WebUIEntites
{
    public class DisCountAreaLanguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid DisCountAreaId { get; set; }
        public DisCountArea DisCountArea { get; set; }
    }
}
