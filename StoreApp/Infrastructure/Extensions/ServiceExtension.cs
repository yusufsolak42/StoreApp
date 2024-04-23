using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) //We use this extension to automate migrations.
        {
            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("sqlconnection"),
                b => b.MigrationsAssembly("StoreApp"));
            });  //we say which database we will use.we get the connection string from appsettings.json file.
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "StoreApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10); //we adjust the timespan of the sessions.
            }); //for sessions as middleware 

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //for session, we can read the session.

            services.AddScoped<Cart>(c => SessionCart.GetCart(c)); //for every user, new cart will be generated. we say call it from the sessionCart. cart will be newed from sessioncart.
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();//when i inject this interface, this concrete class will be used. because we use injection.
            services.AddScoped<IProductRepository, ProductRepository>();//when i inject this interface, this concrete class will be used.
            services.AddScoped<ICategoryRepository, CategoryRepository>();//when i inject this interface, this concrete class will be used. we connect these two things.
            services.AddScoped<IOrderRepository, OrderRepository>();//when i inject this interface, this concrete class will be used. we connect these two things.
        }
        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>(); //so when we initialize this interface, this concrete class will be used. Loosely coupled.
            services.AddScoped<IProductService, ProductManager>();//so when we initialize this interface, this concrete class will be used.Loosely coupled.
            services.AddScoped<ICategoryService, CategoryManager>();//so when we initialize this interface, this concrete class will be used.Loosely coupled.
            services.AddScoped<IOrderService, OrderManager>();//so when we initialize this interface, this concrete class will be used.Loosely coupled.
        }

        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });
        }

    }
}