using System;

namespace SmsGateway.MeApiWrapper {
  public class MesssageData {
    public string DeviceId { get; set; }
    public string ContactId { get; set; }
    public string Number { get; set; }
    public string Message { get; set; }
    public DateTime? SendAt { get; set; }
    public DateTime? ExpiressAt { get; set; }
  }
}