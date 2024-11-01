using Ava.Application.Dtos;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Db;
using MediatR;

namespace Ava.Application.Customers.Commands;

public record AddCustomerCommand(CreateCustomerDto Dto) : IRequest<Customer>;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Customer>
{
    private readonly AvaDbContext _context;

    public AddCustomerCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Dto.Id, request.Dto.FirstName, request.Dto.LastName, request.Dto.PersonalId);

        _context.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }
}