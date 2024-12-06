namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class GetAllCuponDTO
    {
        public Guid CuponId { get; set; }
        public string CuponCode { get; set; }
        public decimal DisCountPercent { get; set; }
        public bool IsActive { get; set; }
        public string? ProductCode { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? UserEmail { get; set; }
    }
}
