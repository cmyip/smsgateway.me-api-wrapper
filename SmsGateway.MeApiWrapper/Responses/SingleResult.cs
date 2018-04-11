// ReSharper disable InconsistentNaming
namespace SmsGateway.MeApiWrapper.Responses {
  public class SingleResult<T> : Response {
    public T result { get; set; }
  }
}