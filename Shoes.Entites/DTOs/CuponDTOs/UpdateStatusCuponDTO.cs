namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class UpdateStatusCuponDTO
    {
        public Guid CuponId { get; set; }
        public bool isActive { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? UserId { get; set; }
       public Guid? ProductId { get; set; }
    }
}
