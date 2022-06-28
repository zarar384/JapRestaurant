using Jap.Services.ProductAPI.Models.Dtos;

namespace Jap.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        //get the product list
        Task<IEnumerable<ProductDto>> GetProducts();
        //get one product
        Task<ProductDto> GetProductById(int productId);
        //get new product or put extist product
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        //delete product
        Task<bool> DeleteProduct(int productId);
    }
}
