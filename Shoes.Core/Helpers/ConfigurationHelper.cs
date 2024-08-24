using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Shoes.Core.Helpers
{
    public static class ConfigurationHelper
    {

    
            public static IConfiguration config;
            public static void Initialize(IConfiguration Configuration)
            {
                config = Configuration;
            }
        

    }
}
