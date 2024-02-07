using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Coptis.Shop.Infrastructure.Entities;

public class TSale
{
    [Key]
    public int SaleId { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int Quantity { get; set; }

    public virtual TUser TUser { get; set; }
    public virtual TProduct TProduct { get; set; }
}
