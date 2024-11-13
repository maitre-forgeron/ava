using Ava.Application.Constants;
using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Customers.Commands;

public record UpdateCustomerCommand(UpdateCustomerDto Dto) : IRequest<Result>;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
{
    private readonly AvaDbContext _context;

    public UpdateCustomerCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == request.Dto.Id, cancellationToken);

        if (customer == null)
        {
            return Result.Failure(CustomerErrors.NotFound);
        }

        customer.Update(request.Dto.FirstName, request.Dto.LastName);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
