using Ava.Application.Models;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Customers.Commands;

public record DeleteCustomerCommand(Guid Id) : IRequest<Result>;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
{
    private readonly AvaDbContext _context;

    public DeleteCustomerCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Customers.Where(c => c.Id == request.Id).ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}
