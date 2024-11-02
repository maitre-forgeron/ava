using Ava.Application.Dtos;
using Ava.Domain.Models.Booking;
using Ava.Domain.Models.Common;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record RejectBookingCommand(BookingActionDto Dto) : IRequest<Result>;

public class RejectBookingCommandHandler : IRequestHandler<RejectBookingCommand, Result>
{
    private readonly AvaDbContext _context;
    public RejectBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RejectBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.Where(b => b.Id == request.Dto.Id).SingleOrDefaultAsync(cancellationToken);

        if (booking == null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var authorizationResult = booking.EnsureTherapistAuthorization(request.Dto.TherapistId);

        if(!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        booking.Reject(request.Dto.TherapistId);

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
