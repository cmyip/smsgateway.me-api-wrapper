// ReSharper disable InconsistentNaming

using System.Text;

namespace SmsGateway.MeApiWrapper.Responses {
  public class Device {
    public string id { get; set; }
    public string name { get; set; }
    public string make { get; set; }
    public string model { get; set; }
    public string number { get; set; }
    public string provider { get; set; }
    public string country { get; set; }
    public string connection_type { get; set; }
    public string battery { get; set; }
    public string signal { get; set; }
    public string wifi { get; set; }
    public string lat { get; set; }
    public string lng { get; set; }
    public ulong last_seen { get; set; }
    public ulong created_at { get; set; }

    public string PrettyPrint() {
      var str = new StringBuilder();
      str.AppendLine("--------------------------------------------------------------------------------");
      str.AppendLine($"  id: {id}");
      str.AppendLine($"  name: {name}");
      str.AppendLine($"  make: {make}");
      str.AppendLine($"  model: {model}");
      str.AppendLine($"  number: {number}");
      str.AppendLine($"  provider: {provider}");
      str.AppendLine($"  country: {country}");
      str.AppendLine($"  connection_type: {connection_type}");
      str.AppendLine($"  battery: {battery}");
      str.AppendLine($"  signal: {signal}");
      str.AppendLine($"  wifi: {wifi}");
      str.AppendLine($"  lat: {lat}");
      str.AppendLine($"  lng: {lng}");
      str.AppendLine("--------------------------------------------------------------------------------");
      return str.ToString();
    }
  }
}