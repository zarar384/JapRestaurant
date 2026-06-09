using Jap.WebWasm.Models;
using Jap.WebWasm.Services.IServices;

namespace Jap.WebWasm.Services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public Task<CartDto?> GetCartByUserIdAsync(string userId) =>
            SendAsync<CartDto>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId
            });

        public Task<ResponseDto?> AddToCartAsync(CartDto cartDto) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart"
            });

        public Task<ResponseDto?> UpdateCartAsync(CartDto cartDto) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart"
            });

        public Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetailsId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            });

        public Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });

        public Task<ResponseDto?> RemoveCouponAsync(string userId) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.DELETE,
                Data = userId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon"
            });

        public Task<ResponseDto?> CheckoutAsync(CartHeaderDto cartHeader) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartHeader,
                Url = SD.ShoppingCartAPIBase + "/api/cart/Checkout"
            });
    }
}
