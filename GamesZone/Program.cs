using GamesZone.Models;
using GamesZone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var ConnectionString = builder.Configuration.GetConnectionString("Local") ?? throw new InvalidOperationException("Invalid Connection String");
var ConnectionString = builder.Configuration.GetConnectionString("Server") ?? throw new InvalidOperationException("Invalid Connection String");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(ConnectionString));
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICategoriesAndDevicesService<CategoriesService>, CategoriesService>();
builder.Services.AddScoped<ICategoriesAndDevicesService<DevicesServise>, DevicesServise>();

builder.Services.AddScoped<IGameServices, GameServices>();
builder.Services.AddScoped<IGameServices, GameServices>();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}");

app.Run();
