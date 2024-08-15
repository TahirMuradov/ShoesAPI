using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entites
{
    public class Category
    {
        public Guid Id { get; set; }
        public List<CategoryLanguage> CategoryLanguages { get; set; }
        public List<SubCategory>? SubCategories { get; set; }

    }
}
