// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapper.Responses {
  public class SendMessageResultFail {
    public string number { get; set; }
    public string message { get; set; }
    public string device { get; set; }
  }
}