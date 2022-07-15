using AutoMapper;
using Jap.Services.ShoppingCartAPI.Models;
using Jap.Services.ShoppingCartAPI.Models.Dto;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>().ReverseMap();
            config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
            config.CreateMap<CartDetailsDto, CartDetails>().ReverseMap();
            config.CreateMap<Cart, CartDto>().ReverseMap();

        });
        return mappingConfig;
    }
}