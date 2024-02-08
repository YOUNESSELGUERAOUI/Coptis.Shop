using Coptis.Shop.Core.Dtos;
using Coptis.Shop.Core.Models;

namespace Coptis.Shop.Core.Interfaces;

public interface ISalesRepository
{
    Task CreateSaleAsync(CreateSaleDto sale);
    Task<IReadOnlyList<Sale>> GetAllSalesAsync();
}
