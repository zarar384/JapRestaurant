namespace Jap.WebWasm.Models
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int? CartHeaderId { get; set; } = 0;
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
