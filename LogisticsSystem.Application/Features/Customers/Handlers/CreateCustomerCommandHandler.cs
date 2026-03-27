using AutoMapper;
using LogisticsSystem.Application.Common;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Shipments.Commands;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Interfaces;
using MediatR;

namespace LogisticsSystem.Application.Features.Shipments.Handlers;

public class CreateShipmentCommandHandler : ICommandHandler<CreateShipmentCommand, ShipmentDto>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateShipmentCommandHandler(
        IShipmentRepository shipmentRepository,
        ICustomerRepository customerRepository,
        IMapper mapper,
        IMediator mediator)
    {
        _shipmentRepository = shipmentRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ShipmentDto> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
    {
        // Check if shipment number already exists
        var exists = await _shipmentRepository.IsShipmentNoExistsAsync(request.ShipmentNo, cancellationToken: cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"Shipment with number {request.ShipmentNo} already exists");
        }

        // Check if shipper exists
        var shipper = await _customerRepository.GetByIdAsync(request.ShipperId, cancellationToken);
        if (shipper == null)
        {
            throw new ArgumentException($"Shipper with id {request.ShipperId} not found");
        }

        // Parse types
        if (!Enum.TryParse<ShipmentType>(request.Type, true, out var shipmentType))
        {
            throw new ArgumentException($"Invalid shipment type: {request.Type}");
        }

        if (!Enum.TryParse<TransportMode>(request.Mode, true, out var transportMode))
        {
            throw new ArgumentException($"Invalid transport mode: {request.Mode}");
        }

        // Create shipment entity
        var shipment = new Shipment(
            request.ShipmentNo,
            shipmentType,
            transportMode,
            request.ShipperId);

        // Set cargo info
        shipment.SetCargoInfo(
            request.GoodsDescription,
            request.Quantity,
            request.Weight,
            request.Volume,
            request.PackageType);

        // Set consignee if provided
        if (request.ConsigneeId.HasValue)
        {
            var consignee = await _customerRepository.GetByIdAsync(request.ConsigneeId.Value, cancellationToken);
            if (consignee == null)
            {
                throw new ArgumentException($"Consignee with id {request.ConsigneeId} not found");
            }
            shipment.SetConsignee(request.ConsigneeId.Value);
        }

        // Save to database
        var created = await _shipmentRepository.AddAsync(shipment, cancellationToken);

        // Return DTO
        return _mapper.Map<ShipmentDto>(created);
    }
}