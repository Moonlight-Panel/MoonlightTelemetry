using MoonlightTelemetry.App.Database.Configuration;
using MoonlightTelemetry.App.Helpers;
using Newtonsoft.Json;

namespace MoonlightTelemetry.App.Services;

public class ConfigService
{
    private ConfigV1 Configuration;

    public ConfigService(StorageService _)
    {
        Reload();
    }

    public void Reload()
    {
        var path = PathBuilder.File("storage", "configs", "config.json");
        
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "{}");
        }

        Configuration = JsonConvert.DeserializeObject<ConfigV1>(
            File.ReadAllText(path)
        ) ?? new ConfigV1();
        
        File.WriteAllText(path, JsonConvert.SerializeObject(Configuration, Formatting.Indented));
    }

    public void Save(ConfigV1 configV1)
    {
        Configuration = configV1;
        Save();
    }

    public void Save()
    {
        var path = PathBuilder.File("storage", "configs", "config.json");
        
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "{}");
        }
        
        File.WriteAllText(path, JsonConvert.SerializeObject(Configuration, Formatting.Indented));
        
        Reload();
    }

    public ConfigV1 Get()
    {
        return Configuration;
    }
}