using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Signal.Server.Entities;

[Index(nameof(MAC), nameof(ApiKey))]
public class AuthorizedSensor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string MAC { get; set; }
    public string ApiKey { get; set; }
    public DateTime CreatedTimeUtc { get; set; }
    public DateTime? UpdatedTimeUtc { get; set; }
}