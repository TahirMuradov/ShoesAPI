﻿namespace Shoes.Entites.DTOs.CategoryDTOs
{
    public class GetCategoryForUIDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Dictionary<Guid, string> SubCategories { get; set; }
    }
}
