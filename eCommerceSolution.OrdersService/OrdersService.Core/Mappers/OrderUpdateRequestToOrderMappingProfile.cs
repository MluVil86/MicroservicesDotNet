using AutoMapper;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.Mappers;

public class OrderUpdateRequestToOrderMappingProfile: Profile
{
    public OrderUpdateRequestToOrderMappingProfile()
    {
        CreateMap<OrderUpdateRequest, Order>()
            .ForMember(dest => dest.OrderID, opt => opt.MapFrom(dest => dest.OrderID))
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(dest => dest.UserID))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(dest => dest.OrderDate))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(dest => dest.OrderItems))
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalBill, opt => opt.Ignore());
    }
}
