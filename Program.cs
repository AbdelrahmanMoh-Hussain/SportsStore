using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection"));
});
builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.MapControllerRoute("pagination", "Products/Page{productPage}", new { controller = "Home", action = "Index" });
app.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { controller = "Home", action = "Index" });
app.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();