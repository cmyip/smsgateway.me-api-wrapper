using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SmsGateway.MeApiWrapperCore
{
    public static class Utils
    {
        public static Uri AttachParameters(this Uri uri, NameValueCollection parameters)
        {
            return parameters.Count == 0 ? uri : new Uri(uri + "?" + parameters.AsHttpParams());
        }

        public static string AsHttpParams(this NameValueCollection parameters)
        {
            var stringBuilder = new StringBuilder();
            var str = "";
            for (var index = 0; index < parameters.Count; ++index)
            {
                stringBuilder.Append(str);
                stringBuilder.Append(HttpUtility.UrlEncode(parameters.AllKeys[index]));
                stringBuilder.Append("=");
                stringBuilder.Append(HttpUtility.UrlEncode(parameters[index]));
                str = "&";
            }
            return stringBuilder.ToString();
        }

        public static IEnumerable<KeyValuePair<string, string>> ToPairs(this NameValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            return collection.Cast<string>().Select(key => new KeyValuePair<string, string>(key, collection[key]));
        }

        public static FormUrlEncodedContent AsUrlEncoded(this NameValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            return new FormUrlEncodedContent(collection.ToPairs());
        }
    }
}