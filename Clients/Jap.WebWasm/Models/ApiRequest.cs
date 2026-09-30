using static Jap.WebWasm.Models.SD;

namespace Jap.WebWasm.Models
{
    public class ApiRequest
    {
        // HTTP method used for the API request.
        public ApiType ApiType { get; set; } = ApiType.GET;

        // Target API endpoint.
        public string Url { get; set; } = string.Empty;

        // Optional request body.
        public object? Data { get; set; }
    }
}
