namespace Shoes.Entites.DTOs.CategoryDTOs
{
    public class GetCategoryDTO
    {
        public Guid Id { get; set; }
        public bool IsFeatured { get; set; }

        public string Content { get; set; }
    }
}
