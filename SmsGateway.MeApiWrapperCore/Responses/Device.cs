// ReSharper disable InconsistentNaming

using Newtonsoft.Json;
using System;
using System.Text;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class Device
    {
        public string id { get; set; }
        public string name { get; set; }
        public string make { get; set; }
        public string model { get; set; }

        [JsonProperty("phone_number")]
        public string phoneNumber { get; set; }

        public string provider { get; set; }
        public string country { get; set; }
        public string connectionType { get; set; }
        public string battery { get; set; }
        public string signalPercent { get; set; }
        public string wifi { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }

        [JsonProperty("last_seen")]
        public DateTime lastSeen { get; set; }

        public string PrettyPrint()
        {
            var str = new StringBuilder();
            str.AppendLine("--------------------------------------------------------------------------------");
            str.AppendLine($"  id: {id}");
            str.AppendLine($"  name: {name}");
            str.AppendLine($"  make: {make}");
            str.AppendLine($"  model: {model}");
            str.AppendLine($"  phoneNumber: {phoneNumber}");
            str.AppendLine($"  provider: {provider}");
            str.AppendLine($"  country: {country}");
            str.AppendLine($"  connectionType: {connectionType}");
            str.AppendLine($"  battery: {battery}");
            str.AppendLine($"  signalPercent: {signalPercent}");
            str.AppendLine($"  wifi: {wifi}");
            str.AppendLine($"  lat: {lat}");
            str.AppendLine($"  lng: {lng}");
            str.AppendLine("--------------------------------------------------------------------------------");
            return str.ToString();
        }
    }
}