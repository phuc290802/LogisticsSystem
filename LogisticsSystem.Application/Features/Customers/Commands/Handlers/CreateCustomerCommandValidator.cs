using FluentValidation;
using LogisticsSystem.Application.Features.Customers.Commands;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Interfaces;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Customer code is required")
            .MaximumLength(20);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required")
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .MaximumLength(20);

        RuleFor(x => x.Type)
            .Must(type => Enum.TryParse<CustomerType>(type, true, out _))
            .WithMessage("Invalid customer type");

        RuleFor(x => x.Code)
            .MustAsync(async (code, cancellation) =>
                !await _customerRepository.IsCodeExistsAsync(code, cancellationToken: cancellation))
            .WithMessage("Customer code already exists");
    }
}