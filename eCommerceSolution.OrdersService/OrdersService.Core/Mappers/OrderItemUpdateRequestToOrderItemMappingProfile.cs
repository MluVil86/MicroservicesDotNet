using AutoMapper;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.Mappers;

public class OrderItemUpdateRequestToOrderItemMappingProfile :Profile
{
    public OrderItemUpdateRequestToOrderItemMappingProfile()
    {
        CreateMap<OrderItemUpdateRequest, OrderItem>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(dest => dest.ProductID))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(dest => dest.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());
    }
}
