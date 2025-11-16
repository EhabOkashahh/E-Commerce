using E_Commerce.Shared.DTOS.Auth;
using E_Commerce.Shared.DTOS.Auth.Login;
using E_Commerce.Shared.DTOS.Auth.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Aabstractions
{
    public interface IAuthServices
    {
        Task<AuthResponse> LoginAsync(LoginDto logRequest);
        Task<AuthResponse> RegisterAsync(RegisterDto regRequest);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<AuthResponse> GetCurrentUserAsync(string email);
        Task<AddressDto> GetCurrentUserAddress(string email);
        Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto request , string email);
        
    }
}
