using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shoes.Core.Entities.Concrete;
using Shoes.Entites;
using Shoes.Entites.WebUIEntites;

namespace Shoes.DataAccess.Concrete.SqlServer
{
    public class AppDBContext:IdentityDbContext<User,AppRole,Guid>
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
        public DbSet<Cupon> Cupons { get; set; }
   
        public DbSet<HomeSliderItem> HomeSliderItems { get; set; }
        public DbSet<HomeSliderLanguage> HomeSliderLanguages { get; set; }
        public DbSet<TopCategoryArea> TopCategoryAreas { get; set; }
        public DbSet<TopCategoryAreaLanguage> TopCategoryAreaLanguages { get; set; }
        public DbSet<DisCountArea> DisCountAreas { get; set; }
        public DbSet<DisCountAreaLanguage> DisCountAreaLanguages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");

            builder.Entity<AppRole>().ToTable("Roles");         
            builder.Entity<SubCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Cupon>()
                .HasOne(Cupon => Cupon.Category)
                .WithMany(Category => Category.Cupons)
                .HasForeignKey(Cupon => Cupon.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            ;

        }
    }
}
