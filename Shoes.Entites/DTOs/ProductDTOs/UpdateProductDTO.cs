using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoes.Entites.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public Guid Id { get; set; }
        [ModelBinder(BinderType =typeof(ModelBindingDictinory<string,string>))]
        public Dictionary<string, string> ProductName { get; set; }
        [ModelBinder(BinderType = typeof(ModelBindingDictinory<string, string>))]

        public Dictionary<string, string> Description { get; set; }
        public List<Guid> SubCategoriesID { get; set; }
        [ModelBinder(BinderType = typeof(ModelBindingDictinory<Guid, int>))]

        public Dictionary<Guid, int> Sizes { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
        public List<string> CurrentPictureUrls { get; set; }
        public IFormFileCollection NewPictures { get; set; }
    }
}
