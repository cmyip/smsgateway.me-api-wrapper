using System;
using System.Threading.Tasks;
using SmsGateway.MeApiWrapper;

namespace TestApp {
  internal class Program {
    public static void Main(string[] args) {
      var smsGateway = new SmsGatewayApi(args[0], args[1]);
      Task.Run(async () => {
        while (true) {
          Console.WriteLine("type a command");
          var readLine = Console.ReadLine();
          if (readLine == null || readLine == "exit") {
            break;
          }
          var strings = readLine.Split(' ');
          switch (strings[0]) {
            case "getDevices":
              Console.WriteLine("get devices");
              if (strings.Length == 2) {
                var result = await smsGateway.GetDevices(Convert.ToInt32(strings[1]));
                Console.WriteLine(result.PrettyPrint());
              } else {
                var result = await smsGateway.GetDevices();
                Console.WriteLine(result.PrettyPrint());
              }
              break;
            case "getDevice":
              if (strings.Length != 2) {
                Console.WriteLine("expected device id");
              } else {
                Console.WriteLine("get device {0}", strings[1]);
                var result = await smsGateway.GetDevice(strings[1]);
                Console.WriteLine(result.PrettyPrint());
              }
              break;
            case "getMessages":
              Console.WriteLine("get messages");
              if (strings.Length == 2) {
                var result = await smsGateway.GetMessages(Convert.ToInt32(strings[1]));
                Console.WriteLine(result.PrettyPrint());
              } else {
                var result = await smsGateway.GetMessages();
                Console.WriteLine(result.PrettyPrint());
              }
              break;
            case "getMessage":
              if (strings.Length != 2) {
                Console.WriteLine("expected message id");
              } else {
                Console.WriteLine("get message {0}", strings[1]);
                var result = await smsGateway.GetMessage(strings[1]);
                Console.WriteLine(result.PrettyPrint());
              }
              break;
          }
        }
      }).GetAwaiter().GetResult();
    }
  }
}