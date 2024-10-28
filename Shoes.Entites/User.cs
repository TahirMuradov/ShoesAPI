using Shoes.Core.Entities.Concrete;

namespace Shoes.Entites
{
    public class User:AppUser
    {
        public List<UserCupon>? Cupons { get; set; }

    }
}
