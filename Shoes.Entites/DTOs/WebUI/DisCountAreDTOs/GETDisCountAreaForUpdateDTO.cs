﻿namespace Shoes.Entites.DTOs.WebUI.DisCountAreDTOs
{
    public class GETDisCountAreaForUpdateDTO
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> TitleContent { get; set; }
        public Dictionary<string, string> DescriptionContent { get; set; }
    }
}
