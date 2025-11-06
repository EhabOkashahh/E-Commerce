using E_Commerce.Services.Aabstractions;
using E_Commerce.Shared.DTOS.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {   
            var UserEmailClaim = User.FindFirst(ClaimTypes.Email);
           var result = await _serviceManager.OrderServices.CreateOrderAsync(request,UserEmailClaim.Value);
           return Ok(result);
        }


        #region Functions
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderForSpecificUserById(Guid id)
        {   
           var UserEmailClaim = User.FindFirst(ClaimTypes.Email)!.Value;
           var result = await _serviceManager.OrderServices.GetOrderByIdForSpecificUserAsync(id,UserEmailClaim);
           return Ok(result);
        }


        [Authorize]
        [HttpGet("delivery-methods")]
        public async Task<IActionResult> GetAllDeliveryMethodAsync()
        {   
           var result = await _serviceManager.OrderServices.GetAllDeliveryMethodsAsync();
            return Ok(result);
        }


        [Authorize]
        [HttpGet("user-orders")]
        public async Task<IActionResult> GeOrdersForSpecificUserAsync()
        {
            var UserEmailClaim = User.FindFirst(ClaimTypes.Email)!.Value;
           var result = await _serviceManager.OrderServices.GetAllOrdersForSpecificUserAsync(UserEmailClaim);
            return Ok(result);
        } 
        #endregion


    }
}
