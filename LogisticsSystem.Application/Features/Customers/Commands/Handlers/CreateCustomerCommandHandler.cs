using AutoMapper;
using LogisticsSystem.Application.Common.Interfaces;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Customers.Commands;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Interfaces;
using MediatR;

namespace LogisticsSystem.Application.Features.Customers.Commands.Handlers;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IMapper mapper,
        IMediator mediator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var exists = await _customerRepository.IsCodeExistsAsync(request.Code, cancellationToken: cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"Customer with code {request.Code} already exists");
        }

        if (!Enum.TryParse<CustomerType>(request.Type, true, out var customerType))
        {
            throw new ArgumentException($"Invalid customer type: {request.Type}");
        }

        var customer = new Customer(
            request.Code,
            request.Name,
            customerType,
            request.TaxCode);


        customer.UpdateInfo(request.Name, request.Address, request.Phone, request.Email);


        var created = await _customerRepository.AddAsync(customer, cancellationToken);


        return _mapper.Map<CustomerDto>(created);
    }
}