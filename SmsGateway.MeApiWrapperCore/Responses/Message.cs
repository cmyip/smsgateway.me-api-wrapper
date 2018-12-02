// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Text;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class MessageLog
    {
        public string status { get; set; }
        public DateTime occurred_at { get; set; }

        public string PrettyPrint()
        {
            var str = new StringBuilder();
            str.AppendLine("----");
            str.AppendLine($"\t occurred_at : {occurred_at}");
            str.AppendLine($"\t status      : {status}");
            str.AppendLine($"----");
            return str.ToString();
        }
    }
    public class Message
    {
        public string id { get; set; }
        public string device_id { get; set; }
        public string phone_number { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public IEnumerable<MessageLog> log { get; set; }
        public string PrettyPrint()
        {
            var str = new StringBuilder();
            str.AppendLine("--------------------------------------------------------------------------------");
            str.AppendLine($"  id: {id}");
            str.AppendLine($"  device_id: {device_id}");
            str.AppendLine($"  phone_number: {phone_number}");
            str.AppendLine($"  message: {message}");
            str.AppendLine($"  status: {status}");
            foreach (MessageLog messageLog in log)
            {
                str.Append(messageLog.PrettyPrint());
            }
            str.AppendLine("--------------------------------------------------------------------------------");
            return str.ToString();
        }
    }
}