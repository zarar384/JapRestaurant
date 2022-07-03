﻿using Jap.Web.Models;

namespace Jap.Web.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetAllProductAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateProduct<T>(ProductDto productDto);
        Task<T> UpdateProductByAsinc<T>(ProductDto productDto);
        Task<T> DeleteProductByAsincAsync<T>(string id);
    }
}
