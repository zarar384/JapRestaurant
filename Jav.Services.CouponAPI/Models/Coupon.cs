using System.ComponentModel.DataAnnotations;

namespace Jav.Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        public string? CouponCode { get; set; } = null;
        public double DiscountAmount { get; set; }
    }
}
