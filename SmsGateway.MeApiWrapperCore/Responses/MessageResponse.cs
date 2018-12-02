using System.Text;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class MessageResponse : SingleResult<Message>
    {
        public string PrettyPrint()
        {
            if (success)
            {
                var str = new StringBuilder();
                str.AppendLine("--------------------------------------------------------------------------------");
                str.AppendLine("Device");
                str.Append(attributes.PrettyPrint());
                str.Append(id);
                str.Append(name);

                return str.ToString();
            }

            return "error";
        }
    }
}