using Jap.WebWasm.Models;
using System.Text;
using System.Text.Json;

namespace Jap.WebWasm.Services
{
    public abstract class BaseService
    {
        protected readonly IHttpClientFactory _httpClientFactory;

        protected BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<T?> SendAsync<T>(ApiRequest request)
        {
            var client = _httpClientFactory.CreateClient("JapApi");
            var message = new HttpRequestMessage();

            // Request and response data are exchanged as JSON.
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(request.Url);

            if (request.Data != null)
            {
                message.Content = new StringContent(
                    JsonSerializer.Serialize(request.Data),
                    Encoding.UTF8,
                    "application/json");
            }

            // Convert the application request type to the corresponding HTTP method.
            message.Method = request.ApiType switch
            {
                SD.ApiType.POST => HttpMethod.Post,
                SD.ApiType.PUT => HttpMethod.Put,
                SD.ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };

            var response = await client.SendAsync(message);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                // Ignore property name casing differences between the API and client models.
                return JsonSerializer.Deserialize<T>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                // Return the default value if the response cannot be deserialized.
                return default;
            }
        }
    }
}