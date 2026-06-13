using AutoMapper;
using Jap.Services.ProductAPI.Models;
using Jap.Services.ProductAPI.Models.Dtos;

namespace Jap.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
