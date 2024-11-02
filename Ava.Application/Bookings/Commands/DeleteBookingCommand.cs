using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record DeleteBookingCommand(Guid Id) : IRequest;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly AvaDbContext _context;
    public DeleteBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Bookings.Where(b => b.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
    }
}
