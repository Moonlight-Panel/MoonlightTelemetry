using MoonlightTelemetry.App.Database;
using MoonlightTelemetry.App.Repositories;
using MoonlightTelemetry.App.Services;

_ = new ConfigService(new()); // Create default config if not existing

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>();

builder.Services.AddSingleton<ConfigService>();
builder.Services.AddSingleton<StorageService>();

builder.Services.AddScoped(typeof(Repository<>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();