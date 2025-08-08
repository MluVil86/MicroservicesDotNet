using AutoMapper;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.Mappers;

public class OrderToOrderResponseMappingProfile: Profile
{
    public OrderToOrderResponseMappingProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.OrderID, opt => opt.MapFrom(dest => dest.OrderID))
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(dest => dest.UserID))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(dest => dest.OrderDate))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(dest => dest.OrderItems))                                                                                   
            .ForMember(dest => dest.TotalBill, opt => opt.MapFrom(dest=> dest.TotalBill))
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.UserPersonName, opt => opt.Ignore());
    }
}
