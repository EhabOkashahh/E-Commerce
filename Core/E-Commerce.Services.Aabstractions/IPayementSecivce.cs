using E_Commerce.Shared.DTOS.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Aabstractions
{
    public interface IPayementSecivce
    {
        Task<BasketDto> CraetePaymentIntentAsync(string basketId);
    }
}
