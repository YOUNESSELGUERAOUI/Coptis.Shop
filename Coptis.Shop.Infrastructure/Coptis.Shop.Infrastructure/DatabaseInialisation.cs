using Coptis.Shop.Infrastructure.Context;
using Coptis.Shop.Infrastructure.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Coptis.Shop.Data;

public static class DatabaseInialisation
{
    public static async void CreateDatabaseIfNotExisits(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<CoptisShopContext>();
            context.Database.EnsureCreated();
            await AddUserAsync(serviceScope);
            await AddSecondUserAsync(serviceScope);
            await AddProductsAsync(context);

        }
    }

    private static async Task AddUserAsync(IServiceScope serviceScope)
    {
        var user = new TUser()
        {
            FirstName = "Youness",
            LastName = "El Gueraoui",
            Email = "yelgueraoui@coptis.com",
            UserName = "yelgueraoui@coptis.com",
            EmailConfirmed = true
        };

        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
        var exitingUser = userManager.FindByEmailAsync(user.Email)?.Result;
        if (exitingUser == null)
        {
            userManager.CreateAsync(user!, "Coptis@2024").Wait();
        }
    }

    private static async Task AddSecondUserAsync(IServiceScope serviceScope)
    {
        var user = new TUser()
        {
            FirstName = "Mahmoud",
            LastName = "AL ABSI",
            Email = "mahmoud.alabsi@coptis.com",
            UserName = "mahmoud.alabsi@coptis.com",
            EmailConfirmed = true
        };

        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
        var exitingUser = userManager.FindByEmailAsync(user.Email)?.Result;
        if (exitingUser == null)
        {
            userManager.CreateAsync(user!, "Coptis@2025").Wait();
        }
    }

    private static async Task AddProductsAsync(CoptisShopContext context)
    {
        var products = new List<TProduct>
        {
            new TProduct { ProductName = "Bouteille d'Eau Écologique", Description = "Bouteille durable de 20oz fabriquée à partir de matériaux recyclés.", Price = 15.99M, StockQuantity = 10 },
            new TProduct { ProductName = "Casque Bluetooth Sans Fil", Description = "Casque à réduction de bruit avec jusqu'à 40 heures d'autonomie.", Price = 89.99M, StockQuantity = 90 },
            new TProduct { ProductName = "Grains de Café Bio", Description = "Sac de 2lb de grains de café bio et équitable, torréfiés localement.", Price = 22.50M, StockQuantity = 0 },
            new TProduct { ProductName = "Lampe de Bureau LED", Description = "Lampe de bureau ajustable avec trois modes de couleur et port de chargement USB.", Price = 34.99M, StockQuantity = 120 },
            new TProduct { ProductName = "Étui pour Smartphone", Description = "Étui résistant aux chocs pour smartphone, disponible en plusieurs couleurs.", Price = 12.99M, StockQuantity = 0 },
            new TProduct { ProductName = "Tapis de Yoga", Description = "Tapis de yoga non glissant et écologique avec sangle de transport.", Price = 25.99M, StockQuantity = 80 },
            new TProduct { ProductName = "Sauce Piquante Gourmet", Description = "Sauce artisanale en petite série avec un coup de pied épicé.", Price = 8.99M, StockQuantity = 250 },
            new TProduct { ProductName = "Mug de Voyage en Acier Inoxydable", Description = "Mug de voyage isolé qui garde les boissons chaudes ou froides pendant des heures.", Price = 19.99M, StockQuantity = 160 },
            new TProduct { ProductName = "Haut-Parleur Bluetooth", Description = "Haut-parleur Bluetooth portable et étanche avec une qualité de son supérieure.", Price = 45.99M, StockQuantity = 110 },
            new TProduct { ProductName = "Chaise de Bureau Ergonomique", Description = "Chaise de bureau confortable et soutenante avec hauteur ajustable et support lombaire.", Price = 129.99M, StockQuantity = 50 }
        };

        if(!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}