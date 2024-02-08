using Coptis.Shop.Core.Dtos;
using Coptis.Shop.Infrastructure.Context;
using Coptis.Shop.Infrastructure.Entities;
using Coptis.Shop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Coptis.Shop.Infrastructure.Tests;

public class ProductRepositoryTests
{
    [Fact]
    public async Task GetProductByIdAsync_ReturnsProduct_WhenProductExists()
    {
        using var context = GetDatabaseContext();
        var repository = new ProductRepository(context);

        var product = await repository.GetProductByIdAsync(1);

        Assert.NotNull(product);
        Assert.Equal(1, product.ProductId);
    }

    [Fact]
    public async Task GetProductByIdAsync_ThrowsInvalidOperationException_WhenProductDoesNotExist()
    {
        using var context = GetDatabaseContext();
        var repository = new ProductRepository(context);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.GetProductByIdAsync(999));
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsAllProducts()
    {
        using var context = GetDatabaseContext();
        var repository = new ProductRepository(context);

        var products = await repository.GetProductsAsync();

        Assert.Equal(2, products.Count);
    }

    [Fact]
    public async Task UpdateStockAsync_UpdatesStockCorrectly()
    {
        using var context = GetDatabaseContext();
        var repository = new ProductRepository(context);
        var saleDto = new CreateSaleDto { ProductId = 1, Quantity = 1 };

        await repository.UpdateStockAsync(saleDto);

        var updatedProduct = await context.Products.FindAsync(1);
        Assert.Equal(99, updatedProduct.StockQuantity);
    }

    [Fact]
    public async Task UpdateStockAsync_DoesNotUpdateStock_WhenQuantityIsTooHigh()
    {
        using var context = GetDatabaseContext();
        var repository = new ProductRepository(context);
        var saleDto = new CreateSaleDto { ProductId = 1, Quantity = 101 };

        await repository.UpdateStockAsync(saleDto);

        var product = await context.Products.FindAsync(1);
        Assert.Equal(100, product.StockQuantity); // Stock quantity remains unchanged
    }

    private CoptisShopContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<CoptisShopContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensure the database is unique per test
            .Options;

        var databaseContext = new CoptisShopContext(options);
        databaseContext.Database.EnsureCreated();

        if (!databaseContext.Products.Any())
        {
            var products = new[]
            {
            new TProduct { ProductId = 1, ProductName = "Test Product 1", Description = "Description 1", Price = 10M, StockQuantity = 100 },
            new TProduct { ProductId = 2, ProductName = "Test Product 2", Description = "Description 2", Price = 20M, StockQuantity = 200 },
            };

            databaseContext.Products.AddRange(products);
            databaseContext.SaveChanges();
        }

        return databaseContext;
    }
}