using AutoMapper;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.Mappers;

public class OrderItemToOrderItemResponseMappingProfile: Profile
{
    public OrderItemToOrderItemResponseMappingProfile()
    {
        CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(dest => dest.ProductID))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(dest => dest.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(dest => dest.TotalPrice));
    }
}

