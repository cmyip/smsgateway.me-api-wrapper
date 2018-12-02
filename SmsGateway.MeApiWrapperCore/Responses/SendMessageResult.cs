// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class SendMessageResult
    {
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
    }
}