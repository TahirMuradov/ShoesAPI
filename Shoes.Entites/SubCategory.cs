using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entites
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public List<SubCategoryLanguage> SubCategoryLanguages { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
