﻿namespace Shoes.Entites.WebUIEntites
{
    public class HomeSliderLanguage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        public Guid HomeSliderItemId { get; set; }
        public HomeSliderItem HomeSliderItem { get; set; }
    }
}
