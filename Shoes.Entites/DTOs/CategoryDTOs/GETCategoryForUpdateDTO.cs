namespace Shoes.Entites.DTOs.CategoryDTOs
{
    public class GETCategoryForUpdateDTO
    {
        public Guid Id { get; set; }
        public bool IsFeatured { get; set; }
        public Dictionary<string, string> Content { get; set; }
    }
}
