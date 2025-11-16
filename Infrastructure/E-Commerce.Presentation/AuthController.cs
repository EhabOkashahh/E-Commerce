using E_Commerce.Services.Aabstractions;
using E_Commerce.Shared.DTOS.Auth;
using E_Commerce.Shared.DTOS.Auth.Login;
using E_Commerce.Shared.DTOS.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request) {
            var loginResponse = await _serviceManager.AuthServices.LoginAsync(request);
            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request) {
            var RegisterResponse = await _serviceManager.AuthServices.RegisterAsync(request);
            return Ok(RegisterResponse);
        }




        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email) { 
           var res = await _serviceManager.AuthServices.CheckEmailExistsAsync(email);
           return Ok(res);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser() {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var res = await _serviceManager.AuthServices.GetCurrentUserAsync(email);
           return Ok(res);
        }

        [HttpGet("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress() {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var res = await _serviceManager.AuthServices.GetCurrentUserAddress(email);
           return Ok(res);
        }

        [HttpPut("Address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto request) {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var res = await _serviceManager.AuthServices.UpdateCurrentUserAddressAsync(request, email);
           return Ok(res);
        }


    }
}
