using Jap.WebWasm.Models;

namespace Jap.WebWasm.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponDto?> GetCouponAsync(string couponCode);
    }
}
