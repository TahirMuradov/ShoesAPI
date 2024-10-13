namespace Shoes.Entites.DTOs.ProductDTOs
{
    public class GetProductFilterParamsDTO
    {
        public Guid CategoryId { get; set; }
        public Guid subCategoryId { get; set; }
        public Guid SizeId { get; set; }
        public int page { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
    }
}
