namespace Shoes.Entites.DTOs.ShippingMethodDTOs
{
    public class GetShippingMethodDTO
    {
        public Guid Id { get; set; }
        public decimal disCount { get; set; }
        public decimal Price { get; set; }

        public string Content { get; set; }
    }
}
