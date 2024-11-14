using Ava.Application.Constants;
using Ava.Application.Contracts;
using Ava.Application.Models;
using Ava.Infrastructure.Db;
using Ava.Infrastructure.Services.Identity;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record ApproveBookingCommand(Guid bookingId) : IRequest<Result>;

public class ApproveBookingCommandHandler : IRequestHandler<ApproveBookingCommand, Result>
{
    private readonly AvaDbContext _context;
    private readonly IUserClaimService _claimService;
    private readonly IAuthService _authService;

    public ApproveBookingCommandHandler(AvaDbContext context, IUserClaimService claimService, IAuthService authService)
    {
        _context = context;
        _claimService = claimService;
        _authService = authService;
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

        var hasTherapistRole = _claimService.HasRoleClaim(CustomerRoles.Therapist);

        if (!hasTherapistRole)
        {
            return Result.Failure(BookingErrors.Unauthorized);
        }

        booking.Approve();
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
