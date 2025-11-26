using CS2Players.Application.Interfaces;
using CS2Players.Application.Services;
using CS2Players.Domain.Interfaces;
using CS2Players.Infrastructure.Data;
using CS2Players.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ITimeRepository, TimeRepository>();
builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();


builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<IJogadorService, JogadorService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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