using Coptis.Shop.Core.Interfaces;
using Coptis.Shop.Core.Models;
using Coptis.Shop.Core.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Coptis.Shop.Core.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductByIdAsync_ReturnsCorrectProduct()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var productService = new ProductService(mockRepository.Object);
        var testProduct = new Product { ProductId = 1, ProductName = "Test Product", Description = "Test Description", Price = 100M, StockQuantity = 10 };

        mockRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(testProduct);

        // Act
        var result = await productService.GetProductByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testProduct.ProductId, result.ProductId);
        Assert.Equal(testProduct.ProductName, result.ProductName);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsAllProducts()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var productService = new ProductService(mockRepository.Object);
        var testProducts = new List<Product>
    {
        new Product { ProductId = 1, ProductName = "Test Product 1", Description = "Test Description 1", Price = 100M, StockQuantity = 10 },
        new Product { ProductId = 2, ProductName = "Test Product 2", Description = "Test Description 2", Price = 200M, StockQuantity = 20 }
    };

        mockRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);

        // Act
        var result = await productService.GetProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.ProductId == 1);
        Assert.Contains(result, p => p.ProductId == 2);
    }
}