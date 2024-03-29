﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO.Ports;
using Signal.Handler;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var serialPortOptions = new SerialPortOptions();
config.Bind(nameof(SerialPortOptions), serialPortOptions);
var sensorInfoOptions = new SensorInfoOptions();
config.Bind(nameof(SensorInfoOptions), sensorInfoOptions);
var serialPort = new SerialPort(serialPortOptions.PortName, serialPortOptions.BaudRate);
serialPort.Open();

if (serialPort.IsOpen)
{
    Console.WriteLine("Listening on port {0}", serialPortOptions.PortName);
}
else
{
    throw new Exception($"Cannot connect to port {config["SerialPort:PortName"]}");
}
while (true)
{
    try
    {
        var status = serialPort.ReadLine();
        switch (status.Trim())
        {
            case "0":
                Console.WriteLine("Door opened!");
                await ServerService.NotifyServerAsync(sensorInfoOptions, DoorStatus.Closed);
                break;
            case "1":
                Console.WriteLine("Door closed!");
                await ServerService.NotifyServerAsync(sensorInfoOptions, DoorStatus.Opened);
                break;
        }
    }
    catch (TimeoutException)
    {
        
    }

    
}