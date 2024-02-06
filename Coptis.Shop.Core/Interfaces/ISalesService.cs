using Coptis.Shop.Core.Dtos;
using Coptis.Shop.Core.Models;

namespace Coptis.Shop.Core.Interfaces
{
    public interface ISalesService
    {
        Task<IReadOnlyList<Sale>> GetAllSalesAsync();

        Task CreateSaleAsync(CreateSaleDto createSaleDto);
    }
}
