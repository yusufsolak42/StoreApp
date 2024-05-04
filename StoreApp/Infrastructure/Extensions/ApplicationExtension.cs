using Microsoft.AspNetCore.Identity;
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

            if (context.Database.GetPendingMigrations().Any()) //if there's migrations, it automatically adds all migrations.
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


        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "admin42";


            //UserManager to create a user
            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            //RoleManager to give all roles to admin
            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateAsyncScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser); //we control if admin exists

            if (user is null) //if there's no admin
            {

                user = new IdentityUser() //we create one admi n
                {
                    Email = "yusuf.solak@outlook.com",
                    PhoneNumber = "123456789",
                    UserName = adminUser,
                };

                var result = await userManager.CreateAsync(user, adminPassword); //we saved it to the database
                
                if (!result.Succeeded)
                    throw new Exception("Admin user could not created.");

                var roleResult = await userManager.AddToRolesAsync(user,
                  roleManager
                    .Roles
                    .Select(r => r.Name)
                    .ToList()
                ); //we assigned the roles
                
                if(!roleResult.Succeeded)
                     throw new Exception("System have problems with role definition");
            }
        }

    }
}