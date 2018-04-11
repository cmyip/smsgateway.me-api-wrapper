// ReSharper disable InconsistentNaming

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
  }
}