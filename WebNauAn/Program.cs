using Microsoft.EntityFrameworkCore;
using WebNauAn.Models;
using WebNauAn.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("WebNauAnContext");
builder.Services.AddDbContext<WebNauAnContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<BuocNauAnRepository>();
builder.Services.AddScoped<NguyenLieuRepository>();
builder.Services.AddScoped<LoaiMonAnRepository>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();