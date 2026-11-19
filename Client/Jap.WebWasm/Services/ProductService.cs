using Jap.WebWasm.Models;
using Jap.WebWasm.Services.IServices;

namespace Jap.WebWasm.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public Task<List<ProductDto>?> GetAllProductAsync() =>
            SendAsync<List<ProductDto>>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/products"
            });

        public Task<ProductDto?> GetProductByIdAsync(int id) =>
            SendAsync<ProductDto>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/products/" + id
            });

        public Task<ResponseDto?> CreateProductAsync(ProductDto productDto) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products"
            });

        public Task<ResponseDto?> UpdateProductAsync(ProductDto productDto) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products"
            });

        public Task<ResponseDto?> DeleteProductAsync(int id) =>
            SendAsync<ResponseDto>(new ApiRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/products/" + id
            });
    }
}
