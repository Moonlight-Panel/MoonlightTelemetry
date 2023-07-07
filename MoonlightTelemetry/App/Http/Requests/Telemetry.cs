namespace MoonlightTelemetry.App.Http.Requests;

public class Telemetry
{
    public string AppUrl { get; set; } = "";
    public int Servers { get; set; }
    public int Nodes { get; set; }
    public int Users { get; set; }
    public int Databases { get; set; }
    public int Webspaces { get; set; }
}