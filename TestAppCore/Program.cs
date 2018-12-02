using SmsGateway.MeApiWrapperCore;
using System;
using System.Threading.Tasks;

namespace TestAppCore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("ApiKey: ");
            var apiKey = Console.ReadLine();

            var smsGateway = new SmsGatewayApi(apiKey);
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.WriteLine("type a command");
                    var readLine = Console.ReadLine();
                    if (readLine == null || readLine == "exit")
                    {
                        break;
                    }
                    var strings = readLine.Split(' ');
                    switch (strings[0])
                    {
                        case "getDevices":
                            Console.WriteLine("get devices");
                            {
                                var result = await smsGateway.GetDevices();
                                Console.WriteLine(result.PrettyPrint());
                            }
                            break;

                        case "getDevice":
                            if (strings.Length != 2)
                            {
                                Console.WriteLine("expected device id");
                            }
                            else
                            {
                                Console.WriteLine("get device {0}", strings[1]);
                                var result = await smsGateway.GetDevice(Convert.ToInt64(strings[1]));
                                Console.WriteLine(result.PrettyPrint());
                            }
                            break;

                        case "getMessages":
                            Console.WriteLine("get messages");
                            if (strings.Length == 2)
                            {
                                var queryParameter = new QueryParameter()
                                {
                                    
                                };
                                var result = await smsGateway.GetMessages(queryParameter);
                                Console.WriteLine(result.PrettyPrint());
                            }
                            else
                            {
                                var queryParameter = new QueryParameter()
                                {

                                };
                                var result = await smsGateway.GetMessages();
                                Console.WriteLine(result.PrettyPrint());
                            }
                            break;

                        case "getMessage":
                            if (strings.Length != 2)
                            {
                                Console.WriteLine("expected message id");
                            }
                            else
                            {
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