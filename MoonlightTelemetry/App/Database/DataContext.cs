using Microsoft.EntityFrameworkCore;
using MoonlightTelemetry.App.Database.Entities;
using MoonlightTelemetry.App.Services;

namespace MoonlightTelemetry.App.Database;

public class DataContext : DbContext
{
    private readonly ConfigService ConfigService;
    
    public DbSet<Moonlight> Moonlights { get; set; }

    public DataContext(ConfigService configService)
    {
        ConfigService = configService;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = ConfigService
                .Get()
                .Database;

            var connectionString = $"host={config.Host};" +
                                   $"port={config.Port};" +
                                   $"database={config.Database};" +
                                   $"uid={config.Username};" +
                                   $"pwd={config.Password}";
            
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.Parse("5.7.37-mysql"),
                builder =>
                {
                    builder.EnableRetryOnFailure(5);
                }
            );
        }
    }
}