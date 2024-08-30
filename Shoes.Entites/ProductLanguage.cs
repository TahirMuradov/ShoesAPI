namespace Shoes.Entites
{
    public  class ProductLanguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
