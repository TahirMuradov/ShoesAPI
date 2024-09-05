using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;


namespace Shoes.Entites.DTOs
{
    public class ModelBindingDictinory<Tkey,TValue> : IModelBinder
    {
        public static T GetValue<T>(String value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {

                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var keyValuePairs = JsonSerializer.Deserialize<List<KeyValuePair<Tkey,TValue>>>(value, options);

                var dictionary = keyValuePairs.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                bindingContext.Result = ModelBindingResult.Success(dictionary);
             
            }
            catch (Exception ex)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}

