namespace Shoes.Entites.DTOs.SubCategoryDTOs
{
    public class GetSubCategoryForUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Dictionary<string,string> Content { get; set; }

    }
}
