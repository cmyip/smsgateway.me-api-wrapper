using System;
using SmsGateway.MeApiWrapperCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace SmsGateway.MeApiTest
{
    public class IntegrationTest
    {
        private readonly ITestOutputHelper _output;
        private SmsGatewayApi gateway;
        private long _targetDeviceId;
        private string _targetPhoneNumber;

        public IntegrationTest(ITestOutputHelper output)
        {
            _output = output;
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string apikey = config.GetSection("apiKey").Value;
            _targetDeviceId = Convert.ToInt64(config.GetSection("targetDeviceId").Value);
            _targetPhoneNumber = config.GetSection("targetPhoneNumber").Value;
            if (apikey.Length == 0)
            {
                throw new Exception(
                    "API Key not configured, copy appsettings.example.json to appsettings.json and populate the values");
            }
            gateway = new SmsGatewayApi(apikey);
        }

        [Fact]
        public async Task GetDevices()
        {
            var devices = await gateway.GetDevices();
            Assert.NotNull(devices);
        }

        [Fact]
        public async Task GetDevice()
        {
            var devices = await gateway.GetDevice(_targetDeviceId);
            Assert.NotNull(devices);
        }

        [Fact]
        public async Task GetMessage()
        {
            var messages = await gateway.GetMessagesByDevice(_targetDeviceId);
            Assert.NotNull(messages);
            _output.WriteLine(messages.PrettyPrint());
        }

        [Fact]
        public async Task SendMessage()
        {
            var messages = await gateway.SendMessage(_targetDeviceId, _targetPhoneNumber, "Test message");
            Assert.NotNull(messages);
            foreach (var message in messages)
            {
                _output.WriteLine(message.PrettyPrint());
            }
        }
    }
}