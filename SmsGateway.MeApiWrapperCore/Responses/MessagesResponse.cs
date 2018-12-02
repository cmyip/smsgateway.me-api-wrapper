using System.Text;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class MessagesResponse
    {
        public Message[] results { get; set; }
        public long count { get; set; }

        public string PrettyPrint()
        {
            var str = new StringBuilder();
            str.AppendLine("--------------------------------------------------------------------------------");
            str.AppendLine($"count {count}");
            str.AppendLine("Messages");
            foreach (var result in results)
            {
                str.Append(result.PrettyPrint());
            }

            return str.ToString();
        }
    }
}