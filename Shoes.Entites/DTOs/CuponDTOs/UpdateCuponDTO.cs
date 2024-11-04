namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class UpdateCuponDTO
    {
        public Guid CuponId { get; set; }
        public decimal DisCountPercent { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid?SubCategoryId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
