using E_Commerce.Services.Aabstractions;
using E_Commerce.Shared.DTOS.Basket;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketByIdAsync(string id)
        {
           var result = await serviceManager.BasketServices.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasketAync(BasketDto basket)
        {
           var result = await serviceManager.BasketServices.CreateBasketAsync(basket);
           return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
           var result = await serviceManager.BasketServices.DeleteBasketAsync(id);
           return NoContent();
        }
    }
}
