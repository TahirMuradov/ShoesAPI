using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entites
{
    public class Size
    {
        public Guid Id { get; set; }
        public int SizeNumber { get; set; }
        public List<SizeProduct> SizeProducts { get; set; }
    }
}
