namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class GetCuponInfoDTO
    {
        public Guid CuponId { get; set; }
        public string CuponCode { get; set; }
        public decimal DisCountPercent { get; set; }
    
        public IEnumerable<Guid>? ProductIDs { get; set; }
        public IEnumerable<Guid>? CategoriesId { get; set; }
        public IEnumerable<Guid>? SubCategories { get; set; }
        public IEnumerable<Guid>? UserId { get; set; }

    }
}
