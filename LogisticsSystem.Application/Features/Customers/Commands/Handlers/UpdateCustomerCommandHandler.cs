using AutoMapper;
using LogisticsSystem.Application.Common.Interfaces;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Customers.Commands;
using LogisticsSystem.Domain.Interfaces;
using MediatR;

namespace LogisticsSystem.Application.Features.Customers.Commands.Handlers;

public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with Id {request.Id} not found");
        }

        customer.UpdateInfo(
            request.Name,
            request.Address,
            request.Phone,
            request.Email
        );

        await _customerRepository.UpdateAsync(customer, cancellationToken);

        return _mapper.Map<CustomerDto>(customer);
    }
}