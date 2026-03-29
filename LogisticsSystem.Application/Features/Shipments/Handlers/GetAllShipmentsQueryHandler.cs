using AutoMapper;
using LogisticsSystem.Application.Common.Interfaces;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Shipments.Queries;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Interfaces;
using MediatR;

namespace LogisticsSystem.Application.Features.Shipments.Handlers;

public class GetAllShipmentsQueryHandler : IQueryHandler<GetAllShipmentsQuery, IReadOnlyList<ShipmentDto>>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IMapper _mapper;

    public GetAllShipmentsQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper)
    {
        _shipmentRepository = shipmentRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ShipmentDto>> Handle(GetAllShipmentsQuery request, CancellationToken cancellationToken)
    {
        var shipments = await _shipmentRepository.GetAllAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.Type))
        {
            if (Enum.TryParse<ShipmentType>(request.Type, true, out var type))
            {
                shipments = shipments.Where(s => s.Type == type).ToList();
            }
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            if (Enum.TryParse<ShipmentStatus>(request.Status, true, out var status))
            {
                shipments = shipments.Where(s => s.Status == status).ToList();
            }
        }

        if (request.ShipperId.HasValue)
        {
            shipments = shipments.Where(s => s.ShipperId == request.ShipperId.Value).ToList();
        }

        if (request.FromDate.HasValue)
        {
            shipments = shipments.Where(s => s.CreatedAt >= request.FromDate.Value).ToList();
        }

        if (request.ToDate.HasValue)
        {
            shipments = shipments.Where(s => s.CreatedAt <= request.ToDate.Value).ToList();
        }

        return _mapper.Map<IReadOnlyList<ShipmentDto>>(shipments);
    }
}