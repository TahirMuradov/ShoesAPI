using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs
{
    public class UpdateHomeSliderItemDTO
    {
        public Guid Id { get; set; }
        [ModelBinder(BinderType =typeof(ModelBindingDictinory<string,string>))]
        public Dictionary<string,string> Title { get; set; }
        [ModelBinder(BinderType = typeof(ModelBindingDictinory<string, string>))]

        public Dictionary<string,string> Description { get; set; }
        public IFormFile? NewImage { get; set; }
        public string? CurrentPictureUrls { get; set; }
    }
}
