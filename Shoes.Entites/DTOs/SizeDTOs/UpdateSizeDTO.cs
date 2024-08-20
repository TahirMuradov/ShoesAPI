using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Entites.DTOs.SizeDTOs
{
    public class UpdateSizeDTO
    {
        public Guid Id { get; set; }
        public int NewSizeNumber { get; set; }
    }
}
