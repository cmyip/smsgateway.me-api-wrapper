// ReSharper disable InconsistentNaming

using System.Text;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class DeviceResponse : SingleResult<Device>
    {
        public string PrettyPrint()
        {
            if (success)
            {
                var str = new StringBuilder();
                str.AppendLine("--------------------------------------------------------------------------------");
                str.AppendLine("Device");
                str.Append(attributes.PrettyPrint());
                return str.ToString();
            }

            return "error";
        }
    }
}