namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class GetCuponInfoDTO
    {
        public Guid CuponId { get; set; }
        public string CuponCode { get; set; }
        public decimal DisCountPercent { get; set; }
    
        public Guid? ProductIDs { get; set; }
        public Guid? CategoriesId { get; set; }
        public Guid? SubCategories { get; set; }
        public Guid? UserId { get; set; }

    }
}
