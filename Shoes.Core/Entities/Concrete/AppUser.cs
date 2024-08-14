using Microsoft.AspNetCore.Identity;

namespace Shoes.Core.Entities.Concrete
{
    public class AppUser:IdentityUser<Guid>
    {
     
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredDate { get; set; }
    }
}
