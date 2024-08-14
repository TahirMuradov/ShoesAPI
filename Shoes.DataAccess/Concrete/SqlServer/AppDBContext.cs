using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shoes.Core.Entities.Concrete;

namespace Shoes.DataAccess.Concrete.SqlServer
{
    public class AppDBContext:IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
    }
}
