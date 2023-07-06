using Newtonsoft.Json;

namespace MoonlightTelemetry.App.Database.Configuration;

public class ConfigV1
{
    [JsonProperty("Database")] public DatabaseData Database { get; set; } = new();

    public class DatabaseData
    {
        [JsonProperty("Host")] public string Host { get; set; } = "";

        [JsonProperty("Port")] public int Port { get; set; } = 3306;

        [JsonProperty("Username")] public string Username { get; set; } = "";

        [JsonProperty("Password")] public string Password { get; set; } = "";

        [JsonProperty("Database")] public string Database { get; set; } = "";
    }
}