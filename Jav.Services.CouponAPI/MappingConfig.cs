using AutoMapper;
using Jap.Services.CouponAPI.Models;
using Jap.Services.CouponAPI.Models.Dto;

namespace Jap.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
