namespace Jap.WebWasm.Models
{
    public static class SD
    {
        // Base URLs for application APIs.
        public static string ProductAPIBase { get; set; } = string.Empty;
        public static string ShoppingCartAPIBase { get; set; } = string.Empty;
        public static string CouponAPIBase { get; set; } = string.Empty;
        public static string IdentityAPIBase { get; set; } = string.Empty;

        // Supported HTTP methods.
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
