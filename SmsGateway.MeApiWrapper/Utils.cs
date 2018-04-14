using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace SmsGateway.MeApiWrapper {
  public static class Utils {
    public static Uri AttachParameters(this Uri uri, NameValueCollection parameters) {
      return parameters.Count == 0 ? uri : new Uri(uri + "?" + parameters.AsHttpParams());
    }

    public static string AsHttpParams(this NameValueCollection parameters) {
      var stringBuilder = new StringBuilder();
      var str = "";
      for (var index = 0; index < parameters.Count; ++index) {
        stringBuilder.Append(str);
        stringBuilder.Append(HttpUtility.UrlEncode(parameters.AllKeys[index]));
        stringBuilder.Append("=");
        stringBuilder.Append(HttpUtility.UrlEncode(parameters[index]));
        str = "&";
      }
      return stringBuilder.ToString();
    }
  }
}