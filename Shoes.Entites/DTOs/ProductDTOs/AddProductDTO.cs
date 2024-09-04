using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoes.Entites.DTOs.ProductDTOs
{

    public class AddProductDTO
    {
        [ModelBinder(BinderType =typeof(ModelBindingDictinory<string,string>))]
        public Dictionary<string,string> ProductName { get; set; }
        [ModelBinder(BinderType = typeof(ModelBindingDictinory<string, string>))]
        public Dictionary<string,string> Description { get; set; }
        [ModelBinder(BinderType = typeof(ModelBindingDictinory<Guid, string>))]
        public Dictionary<Guid, int> Sizes { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
        public List<Guid> SubCategories { get; set; }
        public IFormFileCollection Pictures { get; set; }


    }
}
