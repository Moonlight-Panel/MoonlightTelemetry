using MoonlightTelemetry.App.Helpers;

namespace MoonlightTelemetry.App.Services;

public class StorageService
{
    public StorageService()
    {
        EnsureCreated();
    }
    
    public void EnsureCreated()
    {
        Directory.CreateDirectory(PathBuilder.Dir("storage", "configs"));
    }
}