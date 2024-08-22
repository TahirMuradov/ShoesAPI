namespace Shoes.Entites
{
    public class PaymentMethodLanguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string Content { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
