namespace Shoes.Entites.DTOs.SubCategoryDTOs
{
    public class AddSubCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public Dictionary<string,string>LangContent { get; set; }
    }
}
