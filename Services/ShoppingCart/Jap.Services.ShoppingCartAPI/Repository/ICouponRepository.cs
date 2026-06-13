using Jap.Services.ShoppingCartAPI.Models.Dto;

namespace Jap.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
