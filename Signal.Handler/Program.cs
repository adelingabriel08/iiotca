using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO.Ports;
using Signal.Handler;

using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

await host.RunAsync();

var serialPortOptions = new SerialPortOptions();
config.Bind(nameof(SerialPortOptions), serialPortOptions);

var serialPort = new SerialPort(serialPortOptions.PortName, serialPortOptions.BaudRate);

serialPort.Open();

if (serialPort.IsOpen)
{
    Console.WriteLine("Listening on port {0}", config["SerialPort:PortName"]);
}
else
{
    throw new Exception($"Cannot connect to port {config["SerialPort:PortName"]}");
}
Console.WriteLine("im here");
while (true)
{
    try
    {
        var status = serialPort.ReadLine();
        switch (status)
        {
            case "0":
                Console.WriteLine("Door closed!");
                break;
            case "1":
                Console.WriteLine("Door opened!");
                break;
        }
    }
    catch (TimeoutException)
    {
        
    }

    
}