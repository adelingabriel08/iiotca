using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signal.Server.Attributes;
using Signal.Server.Database;
using Signal.Server.Entities;
using Signal.Server.Models;
using Signal.Server.Services.Contracts;

namespace Signal.Server.ApiControllers;

[ApiController]
[RequireMacAndApiKey]
[Route("track")]
public class TrackController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<TrackController> _logger;

    public TrackController(ApplicationDbContext dbContext, IEmailSender emailSender, ILogger<TrackController> logger)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> TrackSensorEvent([FromBody] TrackSensorEventCmd cmd, [FromHeader] string MAC)
    {
        var sensor = await _dbContext.AuthorizedSensors.FirstOrDefaultAsync(s => s.MAC == MAC);

        if (cmd.CreatedTimeUtc < DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)))
            return BadRequest("Date smaller than 1 day!");

        var entry = new SensorStatusTrack()
            {Status = cmd.Status, CreatedTimeUtc = cmd.CreatedTimeUtc, SensorId = sensor.Id};

        await _dbContext.AddAsync(entry);
        await _dbContext.SaveChangesAsync();

        var emailSent = await _emailSender.SendEmailAsync("adelingabriel08@gmail.com",
            $"Door {cmd.Status.ToString()} at {cmd.CreatedTimeUtc.ToString("dddd, dd MMMM yyyy hh:mm")}",
            $"<h1>Door {cmd.Status.ToString()}</hi><p>Your door was {cmd.Status.ToString()} at {cmd.CreatedTimeUtc.ToString("dddd, dd MMMM yyyy hh:mm")}</p>");
        
        if (!emailSent) _logger.LogWarning($"Cannot send email for entry {entry.Id}!");
        return Ok();
    }
}