using Coptis.Shop.Core.Interfaces;
using Coptis.Shop.Core.Models;

namespace Coptis.Shop.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetProductByIdAsync(productId);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _productRepository.GetProductsAsync();
    }
}
