namespace MoonlightTelemetry.App.Database.Entities;

public class Moonlight
{
    public int Id { get; set; }
    public string AppUrl { get; set; } = "";
    public int Servers { get; set; }
    public int Nodes { get; set; }
    public int Users { get; set; }
    public int Databases { get; set; }
    public int Webspaces { get; set; }
    public DateTime LastHeartbeat { get; set; } = DateTime.UtcNow;
}