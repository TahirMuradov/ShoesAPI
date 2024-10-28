namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class AddCuponForProductDTO
    {
        public Guid ProductId { get; set; }
        public decimal DisCountPercent { get; set; }
    }
}
