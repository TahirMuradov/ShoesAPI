using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Shoes.WebAPI.Services
{
    public class IdentityErrorMessageResource
    {

    }
    public class ErrorMessageService
    {
        private readonly IStringLocalizer _localizer;
        public ErrorMessageService(IStringLocalizerFactory factory)
        {
          
            var type = typeof(IdentityErrorMessageResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(nameof(IdentityErrorMessageResource), assemblyName.Name);
           
   
        }
        public LocalizedString GetKey(string key)
        {          
            return _localizer[key];
        }
    }
}
