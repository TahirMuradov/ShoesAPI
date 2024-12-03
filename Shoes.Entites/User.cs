using Shoes.Core.Entities.Concrete;

namespace Shoes.Entites
{
    public class User:AppUser
    {
        public List<Cupon>? Cupons { get; set; }

    }
}
    