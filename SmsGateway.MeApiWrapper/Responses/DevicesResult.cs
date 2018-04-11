// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapper.Responses {
  public class DevicesResult {
    public int total { get; set; }
    public int per_page { get; set; }
    public int current_page { get; set; }
    public int last_page { get; set; }
    public int from { get; set; }
    public int to { get; set; }
    public Device[] data { get; set; }
  }
}