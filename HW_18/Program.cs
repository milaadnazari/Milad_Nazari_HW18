using HW_18.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGetStores, GetStores>();
builder.Services.AddScoped<IGetStoreProducts, GetStoreProducts>();
builder.Services.AddScoped<IGetProduct, GetProduct>();
builder.Services.AddScoped<IUpdateProduct, UpdateProduct>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
