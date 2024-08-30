using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shoes.Core.Entities.Concrete;
using Shoes.Entites;

namespace Shoes.DataAccess.Concrete.SqlServer
{
    public class AppDBContext:IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SubCategoryLanguage> SubCategoryLanguages { get; set; }
        public DbSet<SubCategoryProduct> SubCategoryProducts { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeProduct> SizeProducts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethodLanguage> PaymentMethodLanguages { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<ShippingMethodLanguage> ShippingMethodLanguages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLanguage> ProductLanguages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("Users");

            builder.Entity<AppRole>().ToTable("Roles");
        }
    }
}
