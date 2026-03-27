using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Shipments.Commands;
using LogisticsSystem.Application.Features.Shipments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ShipmentsController> _logger;

    public ShipmentsController(IMediator mediator, ILogger<ShipmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all shipments
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ShipmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? type,
        [FromQuery] string? status,
        [FromQuery] int? shipperId,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate)
    {
        var query = new GetAllShipmentsQuery
        {
            Type = type,
            Status = status,
            ShipperId = shipperId,
            FromDate = fromDate,
            ToDate = toDate
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get shipment by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ShipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetShipmentByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Get shipment by number
    /// </summary>
    [HttpGet("number/{shipmentNo}")]
    [ProducesResponseType(typeof(ShipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNumber(string shipmentNo)
    {
        var query = new GetShipmentByNoQuery { ShipmentNo = shipmentNo };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Create new shipment
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ShipmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateShipmentCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating shipment");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Update shipment
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ShipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateShipmentCommand command)
    {
        command.Id = id;

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Delete shipment
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteShipmentCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Add container to shipment
    /// </summary>
    [HttpPost("{id}/containers")]
    [ProducesResponseType(typeof(ContainerDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddContainer(int id, [FromBody] AddContainerCommand command)
    {
        command.ShipmentId = id;
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, result);
    }

    /// <summary>
    /// Add tracking event
    /// </summary>
    [HttpPost("{id}/tracking")]
    [ProducesResponseType(typeof(ShipmentTrackingDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddTracking(int id, [FromBody] AddTrackingCommand command)
    {
        command.ShipmentId = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Update customs information
    /// </summary>
    [HttpPatch("{id}/customs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCustoms(int id, [FromBody] UpdateCustomsInfoCommand command)
    {
        command.ShipmentId = id;
        var result = await _mediator.Send(command);
        return Ok(new { updated = result });
    }
}