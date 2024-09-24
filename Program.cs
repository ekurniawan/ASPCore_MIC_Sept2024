using ASPCoreHOL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//menambahkan DI
builder.Services.AddScoped<IRestaurantData, RestaurantADO>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurant}/{action=Index}/{id?}");

app.Run();
