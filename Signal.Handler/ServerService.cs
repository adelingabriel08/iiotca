using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Signal.Handler;

public static class ServerService
{
    public static async Task NotifyServerAsync(SensorInfoOptions sensorInfo, DoorStatus status)
    {
        var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(10);
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(sensorInfo.ServerTrackEndpoint),
            Headers = { 
                { "MAC", sensorInfo.MAC },
                { "APIKEY", sensorInfo.ApiKey },
                { HttpRequestHeader.ContentType.ToString(), "application/json" }
            },
            Content = new StringContent(JsonSerializer.Serialize(new {Status = status, CreatedTimeUtc = DateTime.UtcNow}), Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(httpRequestMessage);
        
        if (response.IsSuccessStatusCode) Console.WriteLine("Server notified successfully!");
        else Console.WriteLine("Cannot reach the server!");
    }
}