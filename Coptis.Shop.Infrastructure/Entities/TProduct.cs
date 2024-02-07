using System.ComponentModel.DataAnnotations;

namespace Coptis.Shop.Infrastructure.Entities;

public class TProduct
{
    [Key]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public virtual ICollection<TSale> TSales { get; set; }
}
