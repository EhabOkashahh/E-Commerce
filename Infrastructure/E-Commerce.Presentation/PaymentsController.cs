using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Services.Specification.Orders;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation
{

    [ApiController]
    [Route("api/[controller]")]
    public class paymentsController(IServiceManager _serviceManager, IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.payementSecivce.CraetePaymentIntentAsync(basketId);
            return Ok(result);
        }

        //stripe listen --forward-to https://localhost:7217/api/payments/webhook
        [Route("webhook")]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_a15daa5f30b49f555ae44d141b81f8b1e32f51685ecc0bbdcf18f210216a5f42";
           
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,signatureHeader, endpointSecret);

            //get order that match the PaymentIntent
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            var paymentIntentID = paymentIntent.Id;
            var spec = new OrderWithPaymentIntentSpecifications(paymentIntentID);
            var order = await unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                order.OrderStatus = OrderStatus.PaymentAccepted;
                await unitOfWork.SaveChangesAsync();
            }
            else if (stripeEvent.Type == EventTypes.InvoicePaymentFailed)
            {
                //update order status
                order.OrderStatus = OrderStatus.PaymentRejected;
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                 Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
             return Ok();
        }
    }
}
    