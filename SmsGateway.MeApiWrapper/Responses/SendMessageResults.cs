// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapper.Responses {
  public class SendMessageResults {
    public SendMessageResult[] success { get; set; }
    public SendMessageResultFail[] fails { get; set; }
  }
}