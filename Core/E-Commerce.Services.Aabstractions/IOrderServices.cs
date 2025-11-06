using E_Commerce.Shared.DTOS.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Aabstractions
{
    public interface IOrderServices
    {
        Task<OrderResponse> CreateOrderAsync(OrderRequest request , string UserEmail);
        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();
        Task<OrderResponse> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail);
        Task<IEnumerable<OrderResponse>> GetAllOrdersForSpecificUserAsync(string UserEmail);
    }
}
