using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.BasketExceptions;
using E_Commerce.Domain.Exceptions.OrderExceptions;
using E_Commerce.Domain.Exceptions.ProductExceptions;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Shared.DTOS.Basket;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = E_Commerce.Domain.Entities.Products.Product;


namespace E_Commerce.Services.Payments
{
    public class PaymentService(IBasketRepository _basketRepository , IUnitOfWork _unitOfWork , IConfiguration _confiig, IMapper _mapper) : IPayementSecivce
    {
        public async Task<BasketDto> CraetePaymentIntentAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }

            var subtotal = basket.Items.Sum(p => p.Price * p.Quantity);

            if (!basket.ShippingMethodId.HasValue) throw new DeliveryMethodNotFound(-1);
            var ShippingMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(basket.ShippingMethodId.Value);
            if (ShippingMethod is null) throw new DeliveryMethodNotFound(basket.ShippingMethodId.Value);
            basket.ShippingCost = ShippingMethod.Price;
            var amount = subtotal + ShippingMethod.Price;

            StripeConfiguration.ApiKey = _confiig["Stripe:SecretKey"];

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(basket.PaymentIntentId is null)
            {
                var CreationOption = new PaymentIntentCreateOptions()
                {
                    Amount = (long)amount * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(CreationOption);
            }
            else
            {
                var CreationOption = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)amount * 100,
                };

                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId,CreationOption);
            }
            
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            basket = await _basketRepository.CreateBasketAsync(basket,TimeSpan.FromDays(1));

            return _mapper.Map<BasketDto>(basket);
        }
    }
}
