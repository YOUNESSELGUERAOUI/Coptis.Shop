using Coptis.Shop.Core.Dtos;
using Coptis.Shop.Core.Models;
using Coptis.Shop.Core.Interfaces;

namespace Coptis.Shop.Core.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IProductRepository _productRepository;

        public SalesService(
            ISalesRepository salesRepository,
            IProductRepository productRepository)
        {
            _salesRepository = salesRepository;
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyList<Sale>> GetAllSalesAsync()
        {
            return await _salesRepository.GetAllSalesAsync();
        }

        public async Task CreateSaleAsync(CreateSaleDto createSaleDto)
        {
            if (createSaleDto == null)
            {
                throw new ArgumentNullException(nameof(createSaleDto));
            }

            await CheckProductStockAsync(createSaleDto);

            await _salesRepository.CreateSaleAsync(createSaleDto);

            await _productRepository.UpdateStockAsync(createSaleDto);
        }

        private async Task<Product> CheckProductStockAsync(CreateSaleDto createSaleDto)
        {
            var product = await _productRepository.GetProductByIdAsync(createSaleDto.ProductId);

            if (product.StockQuantity < createSaleDto.Quantity)
            {
                throw new InvalidOperationException($"La quantité demandée n’est pas disponible pour le moment.");
            }

            return product;
        }
    }
}
