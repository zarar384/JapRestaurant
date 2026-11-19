using Jap.Web.Models;

namespace Jap.Web.Services.IServices
{
    public interface IProductService: IBaseService
    {
        Task<T> GetAllProductAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int id, string token);
        Task<T> CreateProduct<T>(ProductDto productDto, string token);
        Task<T> UpdateProductByAsinc<T>(ProductDto productDto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}
