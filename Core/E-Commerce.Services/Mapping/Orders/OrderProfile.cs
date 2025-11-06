using AutoMapper;
using AutoMapper.Configuration;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Shared.DTOS.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(D => D.OrderId , O => O.MapFrom(S =>S.Id))
                .ForMember(D => D.Total , O=>O.MapFrom(S =>S.GetTotal()))
                .ForMember(D => D.DeliveryMethod , O=>O.MapFrom(S =>S.DeliveryMethod.ShortName));

            CreateMap<OrderAdress, OrderAddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
            .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.Product.ProductUrl));


            CreateMap<DeliveryMethodResponse, DeliveryMethod>().ReverseMap();
        }
    }
}
