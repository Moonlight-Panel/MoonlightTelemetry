using Microsoft.AspNetCore.Mvc;
using MoonlightTelemetry.App.Database.Entities;
using MoonlightTelemetry.App.Http.Requests;
using MoonlightTelemetry.App.Http.Resources;
using MoonlightTelemetry.App.Repositories;

namespace MoonlightTelemetry.App.Http.Controllers;

[ApiController]
[Route("telemetry")]
public class TelemetryController : Controller
{
    private readonly Repository<Moonlight> MoonlightRepository;

    public TelemetryController(Repository<Moonlight> moonlightRepository)
    {
        MoonlightRepository = moonlightRepository;
    }

    [HttpPost]
    public Task<ActionResult> Post([FromBody] Telemetry telemetry)
    {
        var moonlight = MoonlightRepository
            .Get()
            .FirstOrDefault(x => x.AppUrl == telemetry.AppUrl);

        if (moonlight == null)
        {
            MoonlightRepository.Add(new()
            {
                AppUrl = telemetry.AppUrl,
                Databases = telemetry.Databases,
                Nodes = telemetry.Nodes,
                Servers = telemetry.Servers,
                Users = telemetry.Users,
                Webspaces = telemetry.Webspaces
            });
        }
        else
        {
            moonlight.Databases = telemetry.Databases;
            moonlight.Nodes = telemetry.Nodes;
            moonlight.Servers = telemetry.Servers;
            moonlight.Users = telemetry.Users;
            moonlight.Webspaces = telemetry.Webspaces;
            moonlight.LastHeartbeat = DateTime.UtcNow;
            
            MoonlightRepository.Update(moonlight);
        }
        
        return Task.FromResult<ActionResult>(Ok());
    }

    [HttpGet]
    public Task<ActionResult<CompleteTelemetry>> Get()
    {
        var moonlights = MoonlightRepository
            .Get()
            .ToArray();

        var result = new CompleteTelemetry()
        {
            Servers = moonlights.Sum(x => x.Servers),
            Databases = moonlights.Sum(x => x.Databases),
            Nodes = moonlights.Sum(x => x.Nodes),
            Users = moonlights.Sum(x => x.Users),
            Webspaces = moonlights.Sum(x => x.Webspaces)
        };

        return Task.FromResult<ActionResult<CompleteTelemetry>>(
            Ok(result)
        );
    }
}