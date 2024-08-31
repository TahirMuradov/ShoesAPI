using Microsoft.Extensions.DependencyInjection;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.Concrete;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete;

namespace Shoes.Bussines.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddAllScoped(this IServiceCollection services) {
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

        }
    }
}
