using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using SmsGateway.MeApiWrapper.Responses;

namespace SmsGateway.MeApiWrapper {
  public class SmsGatewayApi {
    private const string URL_BASE = "https://smsgateway.me/api/v3/";

    private readonly HttpClient httpClient = new HttpClient();
    
    private readonly string username;
    private readonly string password;

    public SmsGatewayApi(string username, string password) {
      this.username = username;
      this.password = password;
    }

    public async Task<DevicesResponse> GetDevices(int page = 1) {
      var queryParams = DefaultQueryParams();
      if (page > 1) {
        queryParams.Add("page", page.ToString());
      }
      return await Get<DevicesResponse>("devices", queryParams);;
    }

    public async Task<DeviceResponse> GetDevice(string id) {
      return await Get<DeviceResponse>($"devices/view/{id}", DefaultQueryParams());
    }
    
    private async Task<T> Get<T>(string path, NameValueCollection queryParams) where T : class {
      T result = null;
      var response = await httpClient.GetAsync(CreateUri(path, queryParams));
      if (response.IsSuccessStatusCode) {
        result = await response.Content.ReadAsAsync<T>();
      }
      return result;
    }

    private static Uri CreateUri(string path, NameValueCollection queryParams) {
      return new Uri(URL_BASE + path).AttachParameters(queryParams);
    }

    private NameValueCollection DefaultQueryParams() {
      return new NameValueCollection() {
        {"email", username},
        {"password", password}
      };
    }
  }
}