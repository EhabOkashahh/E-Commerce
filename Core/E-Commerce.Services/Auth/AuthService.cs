using AutoMapper;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Domain.Exceptions.AuthExceptions;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.Auth;
using E_Commerce.Shared.DTOS.Auth.Login;
using E_Commerce.Shared.DTOS.Auth.Register;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager, IOptions<JwtOptions> options, IMapper _mapper) : IAuthServices
    {
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null ? throw new ValidationException("This email is already in use") : true;
        }
        public async Task<AuthResponse> GetCurrentUserAsync(string email)
        {
            await CheckEmailExistsAsync(email);
            var user = await _userManager.FindByEmailAsync(email);
            var userToReturn = new AuthResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };
            return userToReturn;
        }

        public async Task<AddressDto> GetCurrentUserAddress(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if(user is null) throw new UserNotFoundException(email);

            var AddressToResturn = _mapper.Map<AddressDto>(user.Address);
            return AddressToResturn;
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto request, string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);

            if(user.Address is null)
            {
                user.Address = _mapper.Map<Address>(request);
            }
            else
            {
                user.Address.FirstName = request.FirstName;
                user.Address.LastName = request.LastName;
                user.Address.City = request.City;
                user.Address.Country = request.Country;
                user.Address.Street = request.Street;
            }
                await _userManager.UpdateAsync(user);

            return _mapper.Map<AddressDto>(user.Address);
        }
        public async Task<AuthResponse> LoginAsync(LoginDto logRequest)
        {
            var LogUser = await _userManager.FindByEmailAsync(logRequest.Email);
            if(LogUser is not null)
            {
               var flag = await _userManager.CheckPasswordAsync(LogUser, logRequest.Password);
                if (!flag) {
                     throw new InvalidEmailOrPasswordException();
                }

                return new AuthResponse()
                {
                    DisplayName = LogUser.DisplayName,
                    Email = logRequest.Email,
                    Token = await GenerateTokenAsync(LogUser)
                };
            }
            throw new InvalidEmailOrPasswordException();
        }

        public async Task<AuthResponse> RegisterAsync(RegisterDto RegRequest)
        {
            var user = new AppUser()
            {
                Email = RegRequest.Email,
                DisplayName = RegRequest.DisplayName,
                //PhoneNumber = RegRequest.Phonenumber,
                UserName = RegRequest.Username
            };

            var creationProcess = await _userManager.CreateAsync(user, RegRequest.Password);
            if (!creationProcess.Succeeded)
            {
                var errors = creationProcess.Errors.Select(e => e.Description);
                throw new ValidationException(errors);
            }
            return new AuthResponse(){ DisplayName = user.DisplayName,
                                       Email = user.Email, 
                                       Token = await GenerateTokenAsync(user)};
        }
        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            var jwtOptions = options.Value;
            var allClaims = new List<Claim>() {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                allClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var Seckey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: allClaims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationDay),
                signingCredentials: new SigningCredentials(Seckey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
