// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapperCore.Responses {
  public class SendMessageResultFail {
    public string number { get; set; }
    public string message { get; set; }
    public string device { get; set; }
  }
}