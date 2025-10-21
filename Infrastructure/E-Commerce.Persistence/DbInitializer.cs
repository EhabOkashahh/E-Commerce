using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            // add Migrations
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) await _context.Database.MigrateAsync();


            //brands
            if (!_context.ProductBrands.Any())
            {
                var BrandsSeed = await File.ReadAllTextAsync(@"..\Infrastructure\E-Commerce.Persistence\Data\DataSeeding\brands.json");
                var BrandsList = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsSeed);

                if (BrandsList is not null && BrandsList.Count > 0)
                {

                    await _context.ProductBrands.AddRangeAsync(BrandsList);
                }
            }

            //types
            if (!_context.ProductTypes.Any())
            {
                var TypeSeed = await File.ReadAllTextAsync(@"..\Infrastructure\E-Commerce.Persistence\Data\DataSeeding\types.json");
                var TypesList = JsonSerializer.Deserialize<List<ProductType>>(TypeSeed);


                if (TypesList is not null && TypesList.Count > 0)
                {

                   await _context.ProductTypes.AddRangeAsync(TypesList);
                }
            }

            //products
            if (!_context.Products.Any())
            {

                var ProductSeed = await File.ReadAllTextAsync(@"..\Infrastructure\E-Commerce.Persistence\Data\DataSeeding\products.json");
                var ProducstList = JsonSerializer.Deserialize<List<Product>>(ProductSeed);

                if (ProducstList is not null && ProducstList.Count > 0)
                {

                   await _context.Products.AddRangeAsync(ProducstList);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
