using E_Commerce.Services.Aabstractions;
using E_Commerce.Shared.DTOS.Auth.Login;
using E_Commerce.Shared.DTOS.Auth.Register;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
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
    }
}
