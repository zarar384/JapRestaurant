namespace Jap.WebWasm.Models
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; } = new();
        public IEnumerable<CartDetailsDto> CartDetails { get; set; } = Enumerable.Empty<CartDetailsDto>();
    }
}
