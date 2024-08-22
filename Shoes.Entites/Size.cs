namespace Shoes.Entites
{
    public class Size
    {
        public Guid Id { get; set; }
        public int SizeNumber { get; set; }
        public List<SoldProduct>? SoldProducts { get; set; }
        public List<SizeProduct>? SizeProducts { get; set; }
    }
}
