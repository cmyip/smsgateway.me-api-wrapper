using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SmsGateway.MeApiWrapperCore.Responses;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmsGateway.MeApiWrapperCore
{
    public class SmsGatewayApi
    {
        private const string URL_BASE = "https://smsgateway.me/api/v4/";
        // private const string URL_BASE = "http://localhost:8000/";

        private readonly HttpClient httpClient;

        private readonly string username;
        private readonly string password;
        private readonly string apiKey;
        private readonly DefaultContractResolver resolver;

        public SmsGatewayApi(string apiKey)
        {
            this.apiKey = apiKey;
            this.httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
            // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(" ", apiKey);
            // httpClient.DefaultRequestHeaders.Add("Content-Type", "Application/json");

            resolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        }

        public async Task<DevicesResponse> GetDevices()
        {
            var g = await Post<DevicesResponse>("device/search", new QueryParameter());
            return g;
        }

        public async Task<DeviceResponse> GetDevice(long id)
        {
            return await Get<DeviceResponse>($"device/{id}");
        }

        public async Task<MessagesResponse> GetMessages(QueryParameter queryParameter)
        {
            return await Post<MessagesResponse>("message/search", queryParameter); ;
        }

        public async Task<MessagesResponse> GetMessagesByDevice(long deviceId)
        {
            var queryParameter = new QueryParameter();
            queryParameter.Filters.Add(new FilterParam()
            {
                Field = "device_id",
                Operator = "=",
                Value = deviceId.ToString()
            });
            return await GetMessages(queryParameter);
        }

        public async Task<MessageResponse> GetMessage(string id)
        {
            return await Get<MessageResponse>($"messages/view/{id}", DefaultQueryParams());
        }
        

        public Task<SendMessageResponse> SendMessageToContact(string deviceId, string contactId, string message, DateTime? sendAt = null, DateTime? expiresAt = null)
        {
            // TODO
            throw new Exception("Method not implemented");
            return SendMessageToContact(deviceId, new[] { contactId }, message, sendAt, expiresAt);
        }

        public async Task<SendMessageResponse> SendMessageToContact(string deviceId, string[] contactIds, string message, DateTime? sendAt = null, DateTime? expiresAt = null)
        {
            throw new Exception("Method not implemented");
            var queryParams = new NameValueCollection();
            queryParams.Add("device", deviceId);
            for (var i = 0; i < contactIds.Length; i++)
            {
                queryParams.Add($"contact[{i}]", contactIds[i]);
            }
            queryParams.Add("message", message);

            if (sendAt != null)
            {
                var sentAtUnixTime = ((DateTimeOffset)sendAt).ToUnixTimeSeconds();
                queryParams.Add("send_at", sentAtUnixTime.ToString());
            }
            if (expiresAt != null)
            {
                var expiresAtUnixTime = ((DateTimeOffset)expiresAt).ToUnixTimeSeconds();
                queryParams.Add("expires_at", expiresAtUnixTime.ToString());
            }

            return await Post<SendMessageResponse>("messages/send", queryParams);
        }

        /// <summary>
        /// Send Message to number
        /// </summary>
        /// <param name="deviceId">Device ID to send from</param>
        /// <param name="number">Phone Number to send to</param>
        /// <param name="message">Content of the message</param>
        /// <returns></returns>
        public Task<IEnumerable<Message>> SendMessage(long deviceId, string number, string message)
        {
            return SendMessage(deviceId, new[] { number }, message);
        }

        /// <summary>
        /// Send Message to list numbers
        /// </summary>
        /// <param name="deviceId">Device ID to send from</param>
        /// <param name="numbers">List of phone numbers to send to</param>
        /// <param name="message">Content of the message</param>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> SendMessage(long deviceId, string[] numbers, string message)
        {
            var messages = numbers.Select(number => new MesssageData()
            {
                DeviceId = deviceId,
                Message = message,
                PhoneNumber = number
            });

            return await Post<IEnumerable<Message>>("message/send", messages);
        }

        public async Task<CreateContactResponse> CreateContact(string name, string number)
        {
            var queryParams = new NameValueCollection() {
        {"name", name},
        {"number", number}
      };
            return await Post<CreateContactResponse>("contacts/create", queryParams);
        }

        public async Task<ListContactsResponse> ListContacts(int page = 1)
        {
            var queryParams = DefaultQueryParams();
            if (page > 1)
            {
                queryParams.Add("page", page.ToString());
            }

            return await Get<ListContactsResponse>("contacts", queryParams);
        }

        public async Task<ContactResponse> GetContact(string id)
        {
            return await Get<ContactResponse>($"contacts/view/{id}", DefaultQueryParams());
        }

        private async Task<T> Get<T>(string path) where T : class
        {
            T result = null;
            var response = await httpClient.GetAsync(CreateUri(path));
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }
            return result;
        }

        private async Task<T> Get<T>(string path, NameValueCollection queryParams) where T : class
        {
            T result = null;
            var response = await httpClient.GetAsync(CreateUri(path, queryParams));
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }
            return result;
        }

        private string JsonSerialize(object source)
        {
            return JsonConvert.SerializeObject(source, new JsonSerializerSettings()
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented
            });
        }

        private async Task<T> Post<T>(string path, QueryParameter query) where T : class
        {
            T result = null;
            string jsonContent = JsonSerialize(query);
            StringContent json = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            //System.Uri uri = CreateUri(path);
            string uri = URL_BASE + path;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = json;

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                bool canParseJson = false;
                string message;
                try
                {
                    string receiveStream = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(receiveStream);
                    message = obj["message"].ToString();
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected response:" + response.ReasonPhrase);
                }
                throw new Exception(message);
            }
            return result;
        }

        //private async Task<T> Post<T>(string path, NameValueCollection queryParams) where T : class
        //{
        //    T result = null;
        //    var response = await httpClient.PostAsync(CreateUri(path, DefaultQueryParams()), queryParams.AsUrlEncoded());
        //    if (response.IsSuccessStatusCode)
        //    {
        //        result = await response.Content.ReadAsAsync<T>();
        //    }
        //    return result;
        //}

        private async Task<T> Post<T>(string path, object postContent) where T : class
        {
            T result = null;
            var stringContent = new StringContent(JsonSerialize(postContent), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(CreateUri(path), stringContent);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }

            return result;
        }

        private static Uri CreateUri(string path)
        {
            return new Uri(URL_BASE + path);
        }

        private static Uri CreateUri(string path, NameValueCollection queryParams)
        {
            return new Uri(URL_BASE + path).AttachParameters(queryParams);
        }

        private NameValueCollection DefaultQueryParams()
        {
            return new NameValueCollection() {
        {"email", username},
        {"password", password}
      };
        }
    }
}