namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class AddCuponForCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public decimal DisCountPercent { get; set; }
    }
}
