using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.BasketExceptions;
using E_Commerce.Domain.Exceptions.OrderExceptions;
using E_Commerce.Domain.Exceptions.ProductExceptions;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Services.Specification.Orders;
using E_Commerce.Shared.DTOS.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork , IMapper _mapper, IBasketRepository _basketRepository) : IOrderServices
    {
        public async Task<OrderResponse> CreateOrderAsync(OrderRequest request, string UserEmail)
        {

            var orderAdderss = _mapper.Map<OrderAdress>(request.ShipToAddress);

            var DeliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (DeliveryMethod is null) throw new DeliveryMethodNotFound(DeliveryMethod.Id);


            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);
            var orderItems = new List<OrderItem>(); 
            foreach (var item in basket.Items)
            {
                var DBproduct = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (DBproduct is null) throw new ProductNotFoundException(item.Id);

                if(DBproduct.Price != item.Price) item.Price = DBproduct.Price;

                var productInOrderItem = new ProductInOrderItems(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem ,item.Price,item.Quantity);
                orderItems.Add(orderItem);
            }

            var SubTotal = orderItems.Sum(i => i.Price * i.Quantity);



            var spec = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
            var result = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);

            if(result is not null)
                _unitOfWork.GetRepository<Guid, Order>().Delete(result);
            


            var Order = new Order(UserEmail, orderAdderss, DeliveryMethod,orderItems, SubTotal , basket.PaymentIntentId);
            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(Order);
            

            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderException();
            _unitOfWork.GetRepository<Guid, Order>().Update(Order);
            await _unitOfWork.SaveChangesAsync();
            var response = _mapper.Map<OrderResponse>(Order);
            return response;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersForSpecificUserAsync(string UserEmail)
        {
            var Specifications = new OrderSpecification(UserEmail);
            var orders = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(Specifications);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
           var Methods = await _unitOfWork.GetRepository<int,DeliveryMethod>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(Methods);
        }

        public async Task<OrderResponse> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail)
        {
            var Specifications = new OrderSpecification(id,UserEmail);
            var orders = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(Specifications);
            return _mapper.Map<OrderResponse>(orders);
        }
    }
}
