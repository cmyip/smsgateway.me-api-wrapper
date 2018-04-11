using System;
using System.Threading.Tasks;
using SmsGateway.MeApiWrapper;

namespace TestApp {
  internal class Program {
    public static void Main(string[] args) {
      var smsGateway = new SmsGatewayApi(args[0], args[1]);
      Console.WriteLine("started");
      Task.Run(async () => {
        Console.WriteLine("get devices");
        var devices = await smsGateway.GetDevices();
        Console.WriteLine("got devices");
        Console.WriteLine(devices.success);
        Console.WriteLine(devices.result.current_page);
        foreach (var device in devices.result.data) {
          Console.WriteLine("device id: {0}", device.id);
        }

        var myDevice = await smsGateway.GetDevice(devices.result.data[0].id);
        Console.WriteLine(myDevice.result.name);
      }).GetAwaiter().GetResult();
    }
  }
}