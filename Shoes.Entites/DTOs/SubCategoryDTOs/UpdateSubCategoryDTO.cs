﻿namespace Shoes.Entites.DTOs.SubCategoryDTOs
{
    public class UpdateSubCategoryDTO
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Dictionary<string,string> LangContent { get; set; }
    }
}
