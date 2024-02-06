using Coptis.Shop.Core.Dtos;
using Coptis.Shop.Core.Models;

namespace Coptis.Shop.Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task UpdateStockAsync(CreateSaleDto createSaleDto);
}
