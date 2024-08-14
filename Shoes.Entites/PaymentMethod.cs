namespace Shoes.Entites
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public List<PaymentMethodLanguage> PaymentMethodLanguages { get; set; }
        public List<Order> Orders { get; set; }
        public bool IsApi { get; set; }

    }
}
