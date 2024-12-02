namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class GetCuponInfoDTO
    {
        public Guid CuponId { get; set; }
        public string CuponCode { get; set; }
        public decimal DisCountPercent { get; set; }
    
        public Guid? ProductID { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? UserId { get; set; }

    }
}
