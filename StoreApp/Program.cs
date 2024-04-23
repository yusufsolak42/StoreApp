
using StoreApp.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



builder.Services.ConfigureDbContext(builder.Configuration); //ConfigureDbContext is the extension method we created.
builder.Services.ConfigureSession(); //we call extension method for session details.
builder.Services.ConfigureRepositoryRegistration(); //Extension method for repos.
builder.Services.ConfigureServiceRegistration(); //Extension method for services.
builder.Services.ConfigureRouting(); // to make url lowercase


builder.Services.AddAutoMapper(typeof(Program)); //registered the mapper to the app. because we use injection.


var app = builder.Build();

app.UseStaticFiles(); //for static files, css and javascript (wwwroot) and other libraries by using libman (client-side stuff)
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
  endpoints.MapAreaControllerRoute(
      name: "Admin",
      areaName: "Admin",
      pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
  );

  endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");

  endpoints.MapRazorPages();


});
app.ConfigureLocalization(); //for local variables like price will be in turkish lira in this app.
app.ConfigureAndCheckMigration(); //this way, we dont need to write "dotnet ef database update" program will update automatically
app.Run();
