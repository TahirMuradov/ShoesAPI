namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class AddCuponForUserDTO
    {
        public Guid UserId { get; set; }
        public decimal DisCountPercent { get; set; }
    }
}
