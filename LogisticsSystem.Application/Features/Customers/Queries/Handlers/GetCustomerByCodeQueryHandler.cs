using AutoMapper;
using LogisticsSystem.Application.DTOs;
using LogisticsSystem.Application.Features.Customers.Queries;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Application.Features.Customers.Queries.Handlers;

public class GetCustomerByCodeQueryHandler : IRequestHandler<GetCustomerByCodeQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByCodeQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetCustomerByCodeQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByCodeAsync(request.Code, cancellationToken);
        return customer == null ? null : _mapper.Map<CustomerDto>(customer);
    }
}
