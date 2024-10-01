using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de Order para OrderDto
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // Mapeamento de OrderItem para OrderItemDto
            CreateMap<OrderItem, OrderItemDto>();

            // Mapeamento inverso dos DTOs para entidades
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Items, opt => opt.Ignore()); // Ignorar para evitar problemas de ciclo

            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.Order, opt => opt.Ignore()); // Ignorar para evitar referências cíclicas
        }
    }
}
