using Jap.Web.Models;
using Jap.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Jap.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                //client object setting
                var client = httpClient.CreateClient("JapApi");
                //send a message in HTTP request format
                HttpRequestMessage message = new HttpRequestMessage();
                //object setting
                message.Headers.Add("Accept", "application/json");
                //Send URI address
                message.RequestUri = new Uri(apiRequest.Url);
                //clear
                client.DefaultRequestHeaders.Clear();

                //serializing data from API Request
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                //get access token
                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }

                //getting a response from the client
                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default: 
                        message.Method=HttpMethod.Get;
                        break;
                }
                //request to the client and create an API request
                apiResponse = await client.SendAsync(message);

                //read like string.
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                    return apiResponseDto;
                }
                catch(JsonReaderException ex)
                {
                    throw new Exception(string.Format($"{apiContent} - {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string>() { Convert.ToString(ex) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }
    }
}
