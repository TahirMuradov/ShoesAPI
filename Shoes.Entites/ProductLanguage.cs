using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entites
{
    public  class ProductLanguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string Information { get; set; }
        public string Title { get; set; }
    }
}
