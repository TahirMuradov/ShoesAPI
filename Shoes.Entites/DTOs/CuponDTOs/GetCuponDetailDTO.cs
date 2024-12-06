namespace Shoes.Entites.DTOs.CuponDTOs
{
    public class GetCuponDetailDTO
    {
        public Guid CuponId { get; set; }
        public string CuponCode { get; set; }
        public bool IsActive { get; set; }
        public decimal DisCountPercent { get; set; }
        public KeyValuePair<Guid,string> ?Product { get; set; }
        public KeyValuePair<Guid,string> ?Category { get; set; }
        public KeyValuePair<Guid,string> ?SubCategory { get; set; }
        public KeyValuePair<Guid,string> ?User { get; set; }
    }
}
