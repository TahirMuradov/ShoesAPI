using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Core.Helpers
{
    public class wwwrootGetPath
    {
        public static string GetwwwrootPath
        {
            get
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                return path;

            }
        }
    }
}
