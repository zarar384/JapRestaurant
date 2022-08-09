using AutoMapper;
using Jav.Services.CouponAPI.DbContexts;
using Jav.Services.CouponAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Jav.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _db;
        protected IMapper _mapper;
        public CouponRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            var couponFromDb = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return _mapper.Map<CouponDto>(couponFromDb);
        }
    }
}
