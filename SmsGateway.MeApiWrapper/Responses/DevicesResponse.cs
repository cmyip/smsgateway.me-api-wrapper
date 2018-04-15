// ReSharper disable InconsistentNaming

using System.Text;

namespace SmsGateway.MeApiWrapper.Responses {
  public class DevicesResponse {
    public bool success { get; set; }
    public DevicesResult result { get; set; }

    public string PrettyPrint() {
      if (success) {
        var str = new StringBuilder();
        str.AppendLine("--------------------------------------------------------------------------------");
        str.AppendLine("Devices");
        str.AppendLine($"Page {result.current_page} of {result.last_page}");
        foreach (var device in result.data) {
          str.Append(device.PrettyPrint());
        }

        return str.ToString();
      }

      return "error";
    }
  }
}