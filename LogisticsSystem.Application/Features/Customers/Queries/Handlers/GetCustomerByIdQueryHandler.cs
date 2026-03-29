using AutoMapper;
using LogisticsSystem.Application.Common.Interfaces;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Customers.Queries;
using LogisticsSystem.Domain.Interfaces;
using MediatR;

namespace LogisticsSystem.Application.Features.Customers.Queries.Handlers;

public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        return customer == null ? null : _mapper.Map<CustomerDto>(customer);
    }
}