using AutoMapper;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogisticsSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Customer mappings
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        // Shipment mappings
        CreateMap<Shipment, ShipmentDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CustomsStatus, opt => opt.MapFrom(src => src.CustomsStatus.ToString()))
            .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.Shipper != null ? src.Shipper.Name : string.Empty))
            .ForMember(dest => dest.ConsigneeName, opt => opt.MapFrom(src => src.Consignee != null ? src.Consignee.Name : string.Empty))
            .ForMember(dest => dest.Containers, opt => opt.MapFrom(src => src.Containers))
            .ForMember(dest => dest.Trackings, opt => opt.MapFrom(src => src.Trackings.OrderByDescending(t => t.EventDate)));

        CreateMap<Container, ContainerDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<ShipmentTracking, ShipmentTrackingDto>()
            .ForMember(dest => dest.EventCode, opt => opt.MapFrom(src => src.EventCode.ToString()));
    }
}