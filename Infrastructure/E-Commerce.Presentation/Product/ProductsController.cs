using E_Commerce.Services.Aabstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var response = await _serviceManager.ProductServices.GetAllProductAsync();
            if (response is null) return BadRequest();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id == null) return BadRequest();
             var response = await _serviceManager.ProductServices.GetProductByIdAsync(id.Value);
            if (response is null) return NotFound();
            return Ok(response);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var response = await _serviceManager.ProductServices.GetAllBrandsAsync();
            if (response is null) return BadRequest();
            return Ok(response);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypesAsync()
        {
            var response = await _serviceManager.ProductServices.GetAllCategoriesAsync();
            if (response is null) return BadRequest();
            return Ok(response);
        }
    }
}
