using FluentValidation;
using LogisticsSystem.Application.Features.Customers.Commands;
using LogisticsSystem.Domain.Interfaces;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellation) =>
                await _customerRepository.GetByIdAsync(id, cancellation) != null)
            .WithMessage("Customer not found");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .MaximumLength(20);
    }
}