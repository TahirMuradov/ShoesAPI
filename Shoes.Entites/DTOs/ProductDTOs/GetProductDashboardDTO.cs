namespace Shoes.Entites.DTOs.ProductDTOs
{
    public class GetProductDashboardDTO
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public List<string> SubCategory { get; set; }
        public Dictionary<int,int> SizeAndCount { get; set; }
        public decimal Price { get; set; }
        public decimal DisCount { get; set; }

    }
}
