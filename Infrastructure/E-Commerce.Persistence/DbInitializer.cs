using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence.Data.Contexts;
using E_Commerce.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    public class DbInitializer(StoreDbContext _context ,
                               StoreIdentityDbContext _identityDbContext , 
                               UserManager<AppUser> _userManager,
                               RoleManager<IdentityRole> _roleManager) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            // add Migrations
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) await _context.Database.MigrateAsync();

            // DeliveryMethods
            if (!_context.DeliveryMethods.Any())
            {
                var seed = await File.ReadAllTextAsync(@"..\Infrastructure\E-Commerce.Persistence\Data\DataSeeding\delivery.json");
                var DeliveryList = JsonSerializer.Deserialize<List<DeliveryMethod>>(seed);
                if(DeliveryList is not null &&  DeliveryList.Count > 0)
                {
                    await _context.AddRangeAsync(DeliveryList);
                }
            }

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

        public async Task InitializeIdentityAsync()
        {
            if (_identityDbContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) await _identityDbContext.Database.MigrateAsync();


            if (!_identityDbContext.Roles.Any())
            {
                var SuperAdmin = new IdentityRole(){Name = "SuperAdmin"};
                var Admin = new IdentityRole(){Name = "Admin"};

                await _roleManager.CreateAsync(SuperAdmin);
                await _roleManager.CreateAsync(Admin);
            }

            if(!_identityDbContext.Users.Any())
            {
                var SuperAdmin = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456342",
                };
                var Admin = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01214523242",
                };

                await _userManager.CreateAsync(SuperAdmin , "P@ssW0rd");
                await _userManager.CreateAsync(Admin, "P@ssW0rd");

                await _userManager.AddToRoleAsync(SuperAdmin , "SuperAdmin");
                await _userManager.AddToRoleAsync(Admin, "Admin");
            }
        }
    }
}
