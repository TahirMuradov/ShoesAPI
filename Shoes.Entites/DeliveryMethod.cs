using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entites
{
    public class DeliveryMethod
    {
        public Guid Id { get; set; }
        public decimal discountPrice { get; set; }
        public decimal price { get; set; }
    }
}
