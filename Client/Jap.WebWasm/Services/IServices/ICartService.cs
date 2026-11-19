using Jap.WebWasm.Models;

namespace Jap.WebWasm.Services.IServices
{
    public interface ICartService
    {
        Task<CartDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> AddToCartAsync(CartDto cartDto);
        Task<ResponseDto?> UpdateCartAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveCouponAsync(string userId);
        Task<ResponseDto?> CheckoutAsync(CartHeaderDto cartHeader);
    }
}
