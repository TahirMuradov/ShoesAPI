using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.Concrete;
using Shoes.Core.Helpers.EmailHelper.Abstract;
using Shoes.Core.Helpers.EmailHelper.Concrete;
using Shoes.Core.Security.Abstarct;
using Shoes.Core.Security.Concrete;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.DataAccess.Concrete;
using Shoes.DataAccess.Concrete.WebUI;

namespace Shoes.Bussines.DependencyResolver
{
    public static class ServiceRegistration
    {

        public static void AddAllScoped(this IServiceCollection services) {
            services.Configure<IdentityOptions>(options =>
            {
                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ISizeService, SizeManager>();
            services.AddScoped<ISizeDAL, EFSizeDAL>();
            services.AddScoped<ISubCategoryService, SubCategoryManager>();
            services.AddScoped<ISubCategoryDAL, EFSubCategoryDAL>();
            services.AddScoped<IPaymentMethodDAL, EFPaymentMethodDAL>();
            services.AddScoped<IPaymentMethodService, PaymentMethodManager>();
            services.AddScoped<IShippingMethodService, ShippingMethodManager>();
            services.AddScoped<IShippingMethodDAL, EFShippingMethodDAL>();
            services.AddScoped<IProductDAL, EFProductDAL>();
            services.AddScoped<IProductService,ProductManager>();
            services.AddScoped<IPictureDAL, EFPictureDAL>();
            services.AddScoped<IPictureService, PictureManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenService, TokenManager>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IHomeSliderItemService, HomeSliderItemManager>();
            services.AddScoped<IHomeSliderItemDAL, EFHomeSliderItemDAL>();
            services.AddScoped<IDisCountAreaDAL, EFDisCountAreaDAL>();
            services.AddScoped<IDisCountAreaService, DisCountAreaManager>();
            services.AddScoped<ITopCategoryAreaDAL, EFTopCategoryAreaDAL>();
            services.AddScoped<ITopCategoryAreaService, TopCategoryAreaManager>();
            services.AddScoped<INewArriwalAreaDAL, EFNewArriwalDAL>();
            services.AddScoped<INewArriwalService, NewArriwalManager>();
            services.AddScoped<ICuponDAL, EFCuponDAL>();
            services.AddScoped<ICuponService,CuponManager>();



        }
    }
}
