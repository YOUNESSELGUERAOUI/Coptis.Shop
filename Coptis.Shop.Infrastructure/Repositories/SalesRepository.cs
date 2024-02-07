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

public class SalesRepository : ISalesRepository
{
    private readonly CoptisShopContext _coptisShopContext;
    private readonly AsyncRetryPolicy _retryPolicy;

    public SalesRepository(CoptisShopContext coptisShopContext)
    {
        _coptisShopContext = coptisShopContext;
        _retryPolicy = Policy.Handle<SqlException>()
            .WaitAndRetryAsync(
                retryCount: 1,
                sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(3000));
    }

    public async Task CreateSaleAsync(CreateSaleDto sale)
    {
        var tSale = new TSale
        {
            UserId = sale.UserId,
            ProductId = sale.ProductId,
            Quantity = sale.Quantity,
            PurchaseDate = DateTime.UtcNow
        };
        tSale.TUser = await GetUserAsync(sale.UserId);
        tSale.TProduct = await GetProductAsync(sale.ProductId);

        await _retryPolicy.ExecuteAsync(async () =>
        {
            await _coptisShopContext.Sales.AddAsync(tSale);
            await _coptisShopContext.SaveChangesAsync();

        }).ConfigureAwait(false);

    }

    public async Task<IReadOnlyList<Sale>> GetAllSalesAsync()
    {
        var sales = new List<TSale>();

        await _retryPolicy.ExecuteAsync(async () =>
        {
            sales = await _coptisShopContext
                                        .Sales
                                        .Include(u => u.TUser)
                                        .Include(t => t.TProduct)
                                        .OrderByDescending(u => u.PurchaseDate)
                                        .ToListAsync();
        }).ConfigureAwait(false);

        return sales?.ToSales();
    }

    private async Task<TUser> GetUserAsync(string userId)
    {
        var tUser = new TUser();
        await _retryPolicy.ExecuteAsync(async () =>
        {
            tUser = await _coptisShopContext
                                        .Users
                                        .FirstOrDefaultAsync(u => u.Id.Equals(userId)) as TUser;
        }).ConfigureAwait(false);

        if (tUser == null)
        {
            throw new InvalidOperationException($"L'utilisateur avec {userId} est introuvable");
        }

        return tUser;
    }

    private async Task<TProduct> GetProductAsync(int productId)
    {
        var tProduct = new TProduct();
        await _retryPolicy.ExecuteAsync(async () =>
        {
            tProduct = await _coptisShopContext
                                        .Products
                                        .FirstOrDefaultAsync(p => p.ProductId == productId);
        }).ConfigureAwait(false);

        if (tProduct == null)
        {
            throw new InvalidOperationException($"Le produit dont l'identifiant est {productId} est introuvable");
        }

        return tProduct;
    }
}
