// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Text;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class DevicesResponse
    {
        public long count { get; set; }
        public IEnumerable<DeviceResponse> results { get; set; }

        public string PrettyPrint()
        {
            var str = new StringBuilder();
            str.AppendLine("--------------------------------------------------------------------------------");
            str.AppendLine("Devices");
            str.AppendLine($"Count {count}");
            foreach (var device in results)
            {
                str.Append(device.PrettyPrint());
            }

            return str.ToString();
        }
    }
}