using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shoes.Entites.DTOs
{
    public class ModelBindingDictinory<Tkey, Tvalue> : IModelBinder
    {
        public static T GetValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));


            var modelName = bindingContext.ModelName;
            var a = bindingContext.HttpContext.Request.Query;
            Dictionary<Tkey, Tvalue> test = new Dictionary<Tkey, Tvalue>();
            foreach (var item in a)
            {
                test.Add(GetValue<Tkey>(item.Key), GetValue<Tvalue>(item.Value));
            }



            try
            {

                bindingContext.Result = ModelBindingResult.Success(test);
            }
            catch (Exception ex)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}

