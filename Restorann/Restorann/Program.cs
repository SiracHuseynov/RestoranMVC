using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restorann.Business.Services.Abstracts;
using Restorann.Business.Services.Concretes;
using Restorann.Core.Models;
using Restorann.Core.RepositoryAbstracts;
using Restorann.Data.DAL;
using Restorann.Data.RepositoryConcretes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")); 
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit= true;
    opt.Password.RequireLowercase= true;
    opt.Password.RequireUppercase= true;

    opt.User.RequireUniqueEmail = false;


}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IChefRepository, ChefRepository>();
builder.Services.AddScoped<IChefService, ChefService>();   

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
