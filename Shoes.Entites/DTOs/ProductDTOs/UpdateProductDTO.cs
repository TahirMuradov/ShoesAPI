﻿using Microsoft.AspNetCore.Http;

namespace Shoes.Entites.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> ProductName { get; set; }
        public Dictionary<string, string> Description { get; set; }
        public List<Guid> SubCategoriesID { get; set; }
        public Dictionary<int, int> Sizes { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
        public List<string> CurrentPictureUrls { get; set; }
        public IFormFileCollection NewPictures { get; set; }
    }
}