namespace Shoes.Entites.DTOs.ProductDTOs
{
    public class GetRelatedProductDTO
    {
        public Guid Id { get; set; }
        public List<string> ImgUrls { get; set; }
        public decimal Price { get; set; }
        public decimal DisCount { get; set; }
        public string Title { get; set; }

    }
}
