﻿namespace Shoes.Entites.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        public Guid Id { get; set; }

        public Dictionary<string,string> Lang { get; set; }
    }
}
