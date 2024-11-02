using Ava.Application.Dtos;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record ApproveBookingCommand(BookingActionDto Dto) : IRequest;

public class ApproveBookingCommandHandler : IRequestHandler<ApproveBookingCommand>
{
    private readonly AvaDbContext _context;
    public ApproveBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApproveBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.Where(b => b.Id == request.Dto.Id).SingleOrDefaultAsync(cancellationToken);

        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found.");
        }

        booking.Approve(request.Dto.TherapistId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
