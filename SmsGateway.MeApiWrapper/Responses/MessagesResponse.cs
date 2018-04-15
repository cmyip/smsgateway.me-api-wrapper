using System.Text;

namespace SmsGateway.MeApiWrapper.Responses {
  public class MessagesResponse : Response {
    public Message[] result { get; set; }
    
    public string PrettyPrint() {
      if (success) {
        var str = new StringBuilder();
        str.AppendLine("--------------------------------------------------------------------------------");
        str.AppendLine("Messages");
        foreach (var device in result) {
          str.Append(device.PrettyPrint());
        }

        return str.ToString();
      }

      return "error";
    }
  }
}