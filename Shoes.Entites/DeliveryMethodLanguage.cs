namespace Shoes.Entites
{
    public class DeliveryMethodLanguage
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string LangCode { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}
