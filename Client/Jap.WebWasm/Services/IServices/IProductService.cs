using Jap.WebWasm.Models;

namespace Jap.WebWasm.Services.IServices
{
    public interface IProductService
    {
        Task<List<ProductDto>?> GetAllProductAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}
