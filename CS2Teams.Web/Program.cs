using Microsoft.EntityFrameworkCore;
using CS2Teams.Application.Interfaces;
using CS2Teams.Application.Services;
using CS2Teams.Application.Mappings;
using CS2Teams.Domain.Interfaces;
using CS2Teams.Infrastructure.Data;
using CS2Teams.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configuração do DbContext com retry policy
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        ));
});

MappingConfig.RegisterMappings();

builder.Services.AddScoped<ITimeRepository, TimeRepository>();
builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<IJogadorService, JogadorService>();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Tentar criar o banco com retry
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var retryCount = 0;
    var maxRetries = 5;
    
    while (retryCount < maxRetries)
    {
        try
        {
            Console.WriteLine($"Tentativa {retryCount + 1} de conectar ao banco...");
            dbContext.Database.EnsureCreated();
            Console.WriteLine("✅ Banco de dados criado com sucesso!");
            break;
        }
        catch (Exception ex)
        {
            retryCount++;
            Console.WriteLine($"❌ Tentativa {retryCount} falhou: {ex.Message}");
            if (retryCount == maxRetries)
            {
                Console.WriteLine("🚨 Todas as tentativas falharam. Verifique se o SQL Server está rodando.");
                throw;
            }
            Thread.Sleep(5000); 
        }
    }
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Times}/{action=Index}/{id?}");

app.Run();
