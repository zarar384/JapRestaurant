using Microsoft.EntityFrameworkCore;

namespace Jap.Services.ShoppingCartAPI.Models
{
    [Keyless]
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
