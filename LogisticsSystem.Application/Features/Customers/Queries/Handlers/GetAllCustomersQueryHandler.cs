using AutoMapper;
using LogisticsSystem.Application.Common;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Customers.Queries;
using LogisticsSystem.Domain.Interfaces;
using MediatR;

namespace LogisticsSystem.Application.Features.Customers.Queries.Handlers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IReadOnlyList<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllCustomersAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<CustomerDto>>(customers);
    }
}