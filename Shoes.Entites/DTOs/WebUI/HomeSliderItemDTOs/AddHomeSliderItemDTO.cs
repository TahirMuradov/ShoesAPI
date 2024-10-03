using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs
{
    public class AddHomeSliderItemDTO
    {
        [ModelBinder(BinderType =typeof(ModelBindingDictinory<string,string>))]
        public Dictionary<string, string> Title { get; set; }
        [ModelBinder(BinderType = typeof(ModelBindingDictinory<string, string>))]

        public Dictionary<string, string> Description { get; set; }
        public IFormFile BackgroundImage { get; set; }

    }
}
