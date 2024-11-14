using Ava.Application.Constants;
using Ava.Application.Contracts;
using Ava.Application.Models;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record RejectBookingCommand(Guid bookingId) : IRequest<Result>;

public class RejectBookingCommandHandler : IRequestHandler<RejectBookingCommand, Result>
{
    private readonly AvaDbContext _context;
    private readonly IUserClaimService _claimService;

    public RejectBookingCommandHandler(AvaDbContext context, IUserClaimService claimService)
    {
        _context = context;
        _claimService = claimService;
    }

    public async Task<Result> Handle(RejectBookingCommand request, CancellationToken cancellationToken)
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

        var hasTherapistRole = _claimService.HasRoleClaim(CustomerRoles.Therapist);

        if (!hasTherapistRole)
        {
            return Result.Failure(BookingErrors.Unauthorized);
        }

        booking.Reject();
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
