using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Core.Entities.Concrete
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
