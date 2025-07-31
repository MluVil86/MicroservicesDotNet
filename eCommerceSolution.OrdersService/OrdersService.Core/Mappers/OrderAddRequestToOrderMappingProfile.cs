using AutoMapper;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.Mappers;

public class OrderAddRequestToOrderMappingProfile: Profile
{
    public OrderAddRequestToOrderMappingProfile()
    {
        CreateMap<OrderAddRequest, Order>()
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(dest => dest.UserID))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(dest => dest.OrderDate))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(dest => dest.OrderItems))
            .ForMember(dest => dest.OrderID, opt => opt.Ignore())
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalBill, opt => opt.Ignore());
    }
}
