namespace Shoes.Entites.DTOs.PaymentMethodDTOs
{
    public class GetPaymentMethodForUpdateDTO
    {
        public Guid Id { get; set; }
        public Dictionary< string,string> Content { get; set; }
        public bool IsApi { get; set; }
    }
}
