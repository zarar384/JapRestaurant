using AutoMapper;
using Jav.Services.CouponAPI.Models;
using Jav.Services.CouponAPI.Models.Dto;

namespace Jav.Services.CouponAPI
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
