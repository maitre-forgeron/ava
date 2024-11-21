using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Domain.Models.User;
using Ava.Infrastructure.Db;
using MediatR;

namespace Ava.Application.Customers.Commands;

public record AddCustomerCommand(CreateCustomerDto Dto) : IRequest<Result>;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Result>
{
    private readonly AvaDbContext _context;

    public AddCustomerCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Dto.Id, request.Dto.FirstName, request.Dto.LastName, request.Dto.PersonalId);

        _context.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}