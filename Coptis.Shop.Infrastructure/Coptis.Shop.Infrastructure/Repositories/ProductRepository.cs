using Coptis.Shop.Core.Dtos;
using Coptis.Shop.Core.Interfaces;
using Coptis.Shop.Core.Models;
using Coptis.Shop.Infrastructure.Context;
using Coptis.Shop.Infrastructure.Entities;
using Coptis.Shop.Infrastructure.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace Coptis.Shop.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CoptisShopContext _coptisShopContext;
    private readonly AsyncRetryPolicy _retryPolicy;

    public ProductRepository(CoptisShopContext coptisShopContext)
    {
        _coptisShopContext = coptisShopContext;
        _retryPolicy = Policy.Handle<SqlException>()
            .WaitAndRetryAsync(
                retryCount: 1,
                sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(3000));
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = new TProduct();

        await _retryPolicy.ExecuteAsync(async () =>
        {
            product = await _coptisShopContext
                                        .Products
                                        .FirstOrDefaultAsync(p => p.ProductId == id);
        }).ConfigureAwait(false);

        if (product is null)
        {
            throw new InvalidOperationException($"Product with id {id} not found in the catalog!");
        }

        return product?.ToProduct();
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var productList = new List<TProduct>();

        await _retryPolicy.ExecuteAsync(async () =>
        {
            productList = await _coptisShopContext
                                        .Products
                                        .ToListAsync();
        }).ConfigureAwait(false);

        return productList.ToProducts();
    }

    public async Task UpdateStockAsync(CreateSaleDto createSaleDto)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            var product = await _coptisShopContext
                                        .Products
                                        .FirstOrDefaultAsync(p => p.ProductId == createSaleDto.ProductId);

            if(product != null && product.StockQuantity >= createSaleDto.Quantity)
            {
                product.StockQuantity -= createSaleDto.Quantity;
                _coptisShopContext.SaveChanges();
            }
        }).ConfigureAwait(false);
    }
}
