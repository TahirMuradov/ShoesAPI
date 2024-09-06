using Microsoft.AspNetCore.Http;

namespace Shoes.Entites.DTOs.ProductDTOs
{
    public class GetDetailProductDashboardDTO
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> ProductName { get; set; }
        public Dictionary<string, string> Description { get; set; }
        public List<GetProductSizeInfoDTO> Sizes { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
        public List<Guid> SubCategories { get; set; }
        public List<string> PictureUrls { get; set; }
    }
}
