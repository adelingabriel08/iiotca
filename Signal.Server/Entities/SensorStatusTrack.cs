using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Signal.Server.Entities;

public class SensorStatusTrack
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DoorStatus Status { get; set; }
    public int SensorId { get; set; }
    public DateTime CreatedTimeUtc { get; set; }
    
    public AuthorizedSensor Sensor { get; set; }
    
}