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
        var result = await smsGateway.SendMessage(myDevice.result.id, args[2], "hello, sent from code...");
        if (result.success) {
          foreach (var fail in result.result.fails) {
            Console.WriteLine("fail {0}", fail.device);
          }
          foreach (var success in result.result.success) {
            Console.WriteLine("success {0}", success.status);
          }
        }
      }).GetAwaiter().GetResult();
    }
  }
}