using System;

namespace SmsGateway.MeApiWrapperCore
{
    public class MesssageData
    {
        public string From { get; set; }
        public long DeviceId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}