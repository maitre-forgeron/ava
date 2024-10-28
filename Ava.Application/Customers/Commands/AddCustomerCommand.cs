using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Customers.Commands;

public record AddCustomerCommand(CreateCustomerDto Dto) : IRequest<Customer>;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Customer>
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Dto.Id, request.Dto.FirstName, request.Dto.LastName, request.Dto.PersonalId);

        await _customerRepository.AddCustomerAsync(customer);

        return customer;
    }
}