using Ava.Application.Dtos;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Customers.Commands;

public record UpdateCustomerCommand(UpdateCustomerDto Dto) : IRequest<Unit>;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly AvaDbContext _context;

    public UpdateCustomerCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == request.Dto.Id, cancellationToken);

        if (customer == null)
        {
            throw new InvalidOperationException();    
        }

        customer.Update(request.Dto.FirstName, request.Dto.LastName);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
