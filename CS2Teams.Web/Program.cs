using CS2Teams.Application.Interfaces;
using CS2Teams.Application.Services;
using CS2Teams.Application.Mappings;
using CS2Teams.Domain.Interfaces;
using CS2Teams.Infrastructure.Data;
using CS2Teams.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


MappingConfig.RegisterMappings();


builder.Services.AddScoped<ITimeRepository, TimeRepository>();
builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<IJogadorService, JogadorService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Times}/{action=Index}/{id?}");

app.Run();
