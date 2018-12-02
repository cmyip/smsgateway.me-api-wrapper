// ReSharper disable InconsistentNaming

using System.Text;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class Contact
    {
        public string id { get; set; }
        public string name { get; set; }
        public string number { get; set; }

        public string PrettyPrint()
        {
            var str = new StringBuilder();
            str.AppendLine(" Contact:");
            str.AppendLine($"  id: {id}");
            str.AppendLine($"  name: {name}");
            str.AppendLine($"  number: {number}");
            return str.ToString();
        }
    }
}