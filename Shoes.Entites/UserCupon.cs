namespace Shoes.Entites
{
    public class UserCupon
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CuponId { get; set; }
        public Cupon Cupon { get; set; }
        public bool isActive { get; set; }
    }
}
