namespace Shoes.Entites.DTOs.CartDTOs
{
    public class CartItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }

    }
}
