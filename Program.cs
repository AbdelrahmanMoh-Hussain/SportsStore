using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection"));
});
builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute("catpage", "{category}/Page{productPage:int}",
 new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page", "Page{productPage:int}",
 new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", "{category}",
 new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination", "Products/Page{productPage}",
 new { Controller = "Home", action = "Index", productPage = 1 });

app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToFile("/admin/{*catchall}", "Admin/Index");

SeedData.EnsurePopulated(app);

app.Run();
