// ReSharper disable InconsistentNaming

using System.Text;

namespace SmsGateway.MeApiWrapper.Responses {
  public class Message {
    public string id { get; set; }
    public string device_id { get; set; }
    public string message { get; set; }
    public string status { get; set; }
    public string send_at { get; set; }
    public string queued_at { get; set; }
    public string sent_at { get; set; }
    public string delivered_at { get; set; }
    public string expires_at { get; set; }
    public string canceled_at { get; set; }
    public string failed_at { get; set; }
    public string received_at { get; set; }
    public string error { get; set; }
    public string created_at { get; set; }
    public Contact contact { get; set; }
    
    public string PrettyPrint() {
      var str = new StringBuilder();
      str.AppendLine("--------------------------------------------------------------------------------");
      str.AppendLine($"  id: {id}");
      str.AppendLine($"  device_id: {device_id}");
      str.AppendLine($"  message: {message}");
      str.AppendLine($"  status: {status}");
      str.AppendLine($"  send_at: {send_at}");
      str.AppendLine($"  queued_at: {queued_at}");
      str.AppendLine($"  sent_at: {sent_at}");
      str.AppendLine($"  delivered_at: {delivered_at}");
      str.AppendLine($"  expires_at: {expires_at}");
      str.AppendLine($"  canceled_at: {canceled_at}");
      str.AppendLine($"  failed_at: {failed_at}");
      str.AppendLine($"  received_at: {received_at}");
      str.AppendLine($"  error: {error}");
      str.AppendLine($"  created_at: {created_at}");
      str.Append(contact.PrettyPrint());
      str.AppendLine("--------------------------------------------------------------------------------");
      return str.ToString();
    }
  }
}