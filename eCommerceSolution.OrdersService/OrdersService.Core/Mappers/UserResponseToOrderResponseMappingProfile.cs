using AutoMapper;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.Mappers;

public class UserResponseToOrderResponseMappingProfile : Profile
{
    public UserResponseToOrderResponseMappingProfile()
    {
        CreateMap<UserResponse, OrderResponse>()
            .ForMember(dest => dest.UserPersonName, opt => opt.MapFrom(dest => dest.PersonName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(dest => dest.Email));
    }
}

