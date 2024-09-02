namespace Shoes.Entites.DTOs.ProductDTOs
{

    public class AddProductDTO
    {      
        public Dictionary<string,string> ProductName { get; set; }      
        public Dictionary<string,string> Description { get; set; }
        public Dictionary<Guid, int> Sizes { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
        public List<Guid> SubCategories { get; set; }
  
     
    }
}
