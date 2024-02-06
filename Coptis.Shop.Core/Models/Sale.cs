namespace Coptis.Shop.Core.Models;

public class Sale
{
    public int SaleId { get; set; }

    public User User { get; set; }

    public Product Product { get; set; }

    public DateTime PurchaseDate { get; set; }

    public int Quantity { get; set; }
}
