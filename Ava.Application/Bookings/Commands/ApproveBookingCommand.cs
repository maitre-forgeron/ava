using Ava.Application.Dtos;
using Ava.Domain.Models.Booking;
using Ava.Domain.Models.Common;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Commands;

public record ApproveBookingCommand(BookingActionDto Dto) : IRequest<Result>;

public class ApproveBookingCommandHandler : IRequestHandler<ApproveBookingCommand, Result>
{
    private readonly AvaDbContext _context;
    public ApproveBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(ApproveBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.Where(b => b.Id == request.Dto.Id).SingleOrDefaultAsync(cancellationToken);

        if (booking == null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        booking.Approve(request.Dto.TherapistId);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
