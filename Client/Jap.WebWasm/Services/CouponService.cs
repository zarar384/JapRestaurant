using Jap.WebWasm.Models;
using Jap.WebWasm.Services.IServices;
using System.Net.Http;

namespace Jap.WebWasm.Services
{
    public class CouponService : BaseService, ICouponService
    {
        public CouponService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public Task<CouponDto?> GetCouponAsync(string couponCode) =>
            SendAsync<CouponDto>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode
            });
    }
}
