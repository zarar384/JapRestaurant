using static Jap.WebWasm.Models.SD;

namespace Jap.WebWasm.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
