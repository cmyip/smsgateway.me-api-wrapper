// ReSharper disable InconsistentNaming

using Newtonsoft.Json;

namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class SingleResult<T> : Response
    {
        public T attributes { get; set; }
        public long id { get; set; }
        public string name { get; set; }

        [JsonProperty("type")]
        public string deviceType { get; set; }
    }
}