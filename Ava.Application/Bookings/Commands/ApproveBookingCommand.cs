using Ava.Application.Constants;
using Ava.Application.Models;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record ApproveBookingCommand(Guid bookingId) : IRequest<Result>;

public class ApproveBookingCommandHandler : IRequestHandler<ApproveBookingCommand, Result>
{
    private readonly AvaDbContext _context;
    public ApproveBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(ApproveBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.Where(b => b.Id == request.bookingId).SingleOrDefaultAsync(cancellationToken);

        if (booking == null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var therapistEntity = await _context.Therapists.Where(t => t.Id == booking.TherapistId).SingleOrDefaultAsync(cancellationToken);

        if (therapistEntity == null)
        {
            return Result.Failure(TherapistErrors.NotFound);
        }

        var hasTherapistRole = await _context.UserRoles
            .Where(r => r.UserId == therapistEntity.Id)
            .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            .AnyAsync(name => name == "therapist", cancellationToken);

        if (!hasTherapistRole)
        {
            return Result.Failure(BookingErrors.Unauthorized);
        }

        booking.Approve();
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
