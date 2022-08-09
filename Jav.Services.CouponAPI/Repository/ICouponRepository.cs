using Jav.Services.CouponAPI.Models.Dto;

namespace Jav.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
