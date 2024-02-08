using Coptis.Shop.Core.Models;
using Coptis.Shop.Infrastructure.Entities;

namespace Coptis.Shop.Infrastructure.Mappers;

public static class MappFromDbToBusiness
{
    public static IReadOnlyList<Product>? ToProducts(this ICollection<TProduct> source)
    {
        return source?.Select(p => p.ToProduct()).ToList();
    }

    public static Product? ToProduct(this TProduct source)
    {
        return source == null ? null :
              new Product
              {
                  ProductId = source.ProductId,
                  ProductName = source.ProductName,
                  Description = source.Description,
                  StockQuantity = source.StockQuantity,
                  Price = source.Price,
              };
    }

    public static User? ToUser(this TUser source)
    {
        return source == null ? null :
              new User
              {
                 UserId = source.Id,
                 FirstName = source.FirstName,
                 LastName = source.LastName,
                 Email = source.Email,
              };
    }


    public static IReadOnlyList<Sale>? ToSales(this ICollection<TSale> source)
    {
        return source?.Select(s => s.ToSale()).ToList();
    }

    public static Sale? ToSale(this TSale source)
    {
        return source == null ? null :
              new Sale
              {
                  SaleId = source.SaleId,
                  Product = source.TProduct?.ToProduct(),
                  User = source.TUser?.ToUser(),
                  PurchaseDate = source.PurchaseDate,
                  Quantity = source.Quantity,
              };
    }
}
