using Signal.Server.Entities;

namespace Signal.Server.Models;

public class TrackSensorEventCmd
{
    public DoorStatus Status { get; set; }
    public DateTime CreatedTimeUtc { get; set; }
}