namespace Shoes.Entites.DTOs.ProductDTOs
{
    public class GetDetailProductDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Dictionary<string?,string> Categories { get; set; }


        public Dictionary<Guid,string> SubCategories { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public decimal DisCount { get; set; }
        public List<string> ImgUrls { get; set; }
        public List<GetProductSizeInfoDTO> Size { get; set; }
        public List<GetRelatedProductDTO> RelatedProducts { get; set; }

    }
}
