using Ava.Application.Dtos;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record RejectBookingCommand(BookingActionDto Dto) : IRequest;

public class RejectBookingCommandHandler : IRequestHandler<RejectBookingCommand>
{
    private readonly AvaDbContext _context;
    public RejectBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RejectBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.Where(b => b.Id == request.Dto.Id).SingleOrDefaultAsync(cancellationToken);

        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found.");
        }

        booking.Reject(request.Dto.TherapistId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
