using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signal.Server.Attributes;
using Signal.Server.Database;
using Signal.Server.Entities;
using Signal.Server.Models;

namespace Signal.Server.ApiControllers;

[ApiController]
[RequireMacAndApiKey]
[Route("track")]
public class TrackController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public TrackController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> TrackSensorEvent([FromBody] TrackSensorEventCmd cmd, [FromHeader] string MAC)
    {
        var sensor = await _dbContext.AuthorizedSensors.FirstOrDefaultAsync(s => s.MAC == MAC);

        var entry = new SensorStatusTrack()
            {Status = cmd.Status, CreatedTimeUtc = cmd.CreatedTimeUtc, SensorId = sensor.Id};

        await _dbContext.AddAsync(entry);
        await _dbContext.SaveChangesAsync();
        
        return Ok();
    }
}