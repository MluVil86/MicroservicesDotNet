using AutoMapper;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.Mappers;

public class ProductResponseToOrderItemResponseMappingProfile : Profile
{
    public ProductResponseToOrderItemResponseMappingProfile()
    {
        CreateMap<ProductResponse, OrderItemResponse>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(dest => dest.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(dest => dest.Category));
    }
}

