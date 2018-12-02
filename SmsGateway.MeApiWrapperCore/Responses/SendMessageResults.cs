// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class SendMessageResults
    {
        public long Id { get; set; }

        public long DeviceId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public MessageLog Log { get; set; }
    }
}