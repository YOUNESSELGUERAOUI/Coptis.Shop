using Coptis.Shop.Core.Models;

namespace Coptis.Shop.Core.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(int productId);
    }
}
