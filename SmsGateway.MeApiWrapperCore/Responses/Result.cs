// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapperCore.Responses
{
    public class Result<T>
    {
        public int count { get; set; }
        public int total { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public T[] results { get; set; }
    }
}