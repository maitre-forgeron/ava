using Ava.Domain.Models.Booking;
using Ava.Domain.Models.Common;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record DeleteBookingCommand(Guid Id) : IRequest<Result>;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Result>
{
    private readonly AvaDbContext _context;
    public DeleteBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingExists = await _context.Bookings.AnyAsync(b => b.Id == request.Id);

        if (!bookingExists)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        await _context.Bookings.Where(b => b.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return Result.Success();
    }
}
