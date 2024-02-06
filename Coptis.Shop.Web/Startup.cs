using Coptis.Shop.Areas.Identity;
using Coptis.Shop.Core.Interfaces;
using Coptis.Shop.Core.Services;
using Coptis.Shop.Data;
using Coptis.Shop.IdentityUtils;
using Coptis.Shop.Infrastructure.Context;
using Coptis.Shop.Infrastructure.Entities;
using Coptis.Shop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Coptis.Shop;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<CoptisShopContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CoptisShopConnection")));
        builder.Services.AddDefaultIdentity<TUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<CoptisShopContext>();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<TUser>>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ISalesService, SalesService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ISalesRepository, SalesRepository>();

        var app = builder.Build();
        app.CreateDatabaseIfNotExisits();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<CookieLoginMiddleware<TUser>>();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
