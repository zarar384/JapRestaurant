using Jap.Web.Models;

namespace Jap.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<T> GetCouponAsnyc<T>(string couponCode, string token = null);
    }
}
