using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app) //we can also extent "WebApplication", IApplicationBuilder is an interface used by webapplication.)
        {
            RepositoryContext context = app
                 .ApplicationServices
                 .CreateScope()
                 .ServiceProvider
                 .GetRequiredService<RepositoryContext>();

            if(context.Database.GetPendingMigrations().Any()) //automatic migrations
            {
                context.Database.Migrate();
            }
        }
    
    public static void ConfigureLocalization(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            options.AddSupportedCultures("tr-TR")
              .AddSupportedUICultures("tr-TR")
              .SetDefaultCulture("tr-TR");
        });
    }
    
    }
}