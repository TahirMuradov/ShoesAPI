namespace Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs
{
    public class GetTopCategoryAreaForUpdateDTO
    {
        public Guid Id { get; set; }


        public Dictionary<string, string> Title { get; set; }
       

        public Dictionary<string, string> Description { get; set; }
        public Guid? CategoryId { get; set; }
        public string PictureUrl { get; set; }
  
    }
}
