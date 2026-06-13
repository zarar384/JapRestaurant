using Jap.Services.CouponAPI.Models.Dto;

namespace Jap.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
