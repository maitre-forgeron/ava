using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Customers.Commands;

public record DeleteCustomerCommand(Guid Id) : IRequest<Unit>;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly AvaDbContext _context;

    public DeleteCustomerCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Customers.Where(c => c.Id == request.Id).ExecuteDeleteAsync(cancellationToken);

        return Unit.Value;
    }
}
