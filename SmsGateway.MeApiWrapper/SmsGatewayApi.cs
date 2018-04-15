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

    public async Task<MessagesResponse> GetMessages(int page = 1) {
      var queryParams = DefaultQueryParams();
      if (page > 1) {
        queryParams.Add("page", page.ToString());
      }
      return await Get<MessagesResponse>("messages", queryParams);;
    }
    
    public async Task<MessageResponse> GetMessage(string id) {
      return await Get<MessageResponse>($"messages/view/{id}", DefaultQueryParams());
    }
    
    public async Task<SendMessageResponse> SendMessages(MesssageData[] data) {
      var queryParams = DefaultQueryParams();
      for (var i = 0; i < data.Length; i++) {
        queryParams.Add($"data[{i}][device]", data[i].DeviceId);
        queryParams.Add($"data[{i}][message]", data[i].Message);

        if (data[i].Number != null) {
          queryParams.Add($"data[{i}][number]", data[i].Number);
        } else if (data[i].ContactId != null) {
          queryParams.Add($"data[{i}][contact]", data[i].ContactId);
        }
      
        if (data[i].SendAt != null) {
          var sentAtUnixTime = ((DateTimeOffset) data[i].SendAt).ToUnixTimeSeconds();
          queryParams.Add($"data[{i}][send_at]", sentAtUnixTime.ToString());
        }
        if (data[i].ExpiressAt != null) {
          var expiresAtUnixTime = ((DateTimeOffset) data[i].ExpiressAt).ToUnixTimeSeconds();
          queryParams.Add($"data[{i}][expires_at]", expiresAtUnixTime.ToString());
        }
      }
      
      return await Post<SendMessageResponse>("messages/send", queryParams);
    }

    public Task<SendMessageResponse> SendMessageToContact(string deviceId, string contactId, string message, DateTime? sendAt = null, DateTime? expiresAt = null) {
      return SendMessageToContact(deviceId, new[] {contactId}, message, sendAt, expiresAt);
    }

    public async Task<SendMessageResponse> SendMessageToContact(string deviceId, string[] contactIds, string message, DateTime? sendAt = null, DateTime? expiresAt = null) {
      var queryParams = new NameValueCollection();
      queryParams.Add("device", deviceId);
      for (var i = 0; i < contactIds.Length; i++) {
        queryParams.Add($"contact[{i}]", contactIds[i]);
      }
      queryParams.Add("message", message);
      
      if (sendAt != null) {
        var sentAtUnixTime = ((DateTimeOffset) sendAt).ToUnixTimeSeconds();
        queryParams.Add("send_at", sentAtUnixTime.ToString());
      }
      if (expiresAt != null) {
        var expiresAtUnixTime = ((DateTimeOffset) expiresAt).ToUnixTimeSeconds();
        queryParams.Add("expires_at", expiresAtUnixTime.ToString());
      }
      
      return await Post<SendMessageResponse>("messages/send", queryParams);
    }

    public Task<SendMessageResponse> SendMessage(string deviceId, string number, string message, DateTime? sendAt = null, DateTime? expiresAt = null) {
      return SendMessage(deviceId, new[] {number}, message, sendAt, expiresAt);
    }

    public async Task<SendMessageResponse> SendMessage(string deviceId, string[] numbers, string message, DateTime? sendAt = null, DateTime? expiresAt = null) {
      var queryParams = new NameValueCollection();
      queryParams.Add("device", deviceId);
      for (var i = 0; i < numbers.Length; i++) {
        queryParams.Add($"number[{i}]", numbers[i]);
      }
      queryParams.Add("message", message);
      
      if (sendAt != null) {
        var sentAtUnixTime = ((DateTimeOffset) sendAt).ToUnixTimeSeconds();
        queryParams.Add("send_at", sentAtUnixTime.ToString());
      }
      if (expiresAt != null) {
        var expiresAtUnixTime = ((DateTimeOffset) expiresAt).ToUnixTimeSeconds();
        queryParams.Add("expires_at", expiresAtUnixTime.ToString());
      }
      
      return await Post<SendMessageResponse>("messages/send", queryParams);
    }

    public async Task<CreateContactResponse> CreateContact(string name, string number) {
      var queryParams = new NameValueCollection() {
        {"name", name},
        {"number", number}
      };
      return await Post<CreateContactResponse>("contacts/create", queryParams);
    }

    public async Task<ListContactsResponse> ListContacts(int page = 1) {
      var queryParams = DefaultQueryParams();
      if (page > 1) {
        queryParams.Add("page", page.ToString());
      }

      return await Get<ListContactsResponse>("contacts", queryParams);
    }
    
    public async Task<ContactResponse> GetContact(string id) {
      return await Get<ContactResponse>($"contacts/view/{id}", DefaultQueryParams());
    }
    
    private async Task<T> Get<T>(string path, NameValueCollection queryParams) where T : class {
      T result = null;
      var response = await httpClient.GetAsync(CreateUri(path, queryParams));
      if (response.IsSuccessStatusCode) {
        result = await response.Content.ReadAsAsync<T>();
      }
      return result;
    }

    private async Task<T> Post<T>(string path, NameValueCollection queryParams) where T : class {
      T result = null;
      var response = await httpClient.PostAsync(CreateUri(path, DefaultQueryParams()), queryParams.AsUrlEncoded());
      if (response.IsSuccessStatusCode) {
        result = await response.Content.ReadAsAsync<T>();
      }
      return result;
    }

    private static Uri CreateUri(string path) {
      return new Uri(URL_BASE + path);
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