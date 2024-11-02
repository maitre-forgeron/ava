using Ava.Application.Dtos;
using Ava.Infrastructure.Db;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Ava.Application.Bookings.Queries;

public record GetBookingQuery(Guid Id) : IRequest<BookingDto>;

public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingDto>
{
    private readonly AvaDbContext _context;

    public GetBookingQueryHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<BookingDto> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == request.Id)
            .Select(b => new BookingDto(
                b.Id,
                b.ConsumerId,
                b.TherapistId,
                b.PreferredTime,
                b.Duration,
                b.Status,
                b.StatusChangeTime
                ))
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (booking == null)
        {
            throw new InvalidOperationException("Booking was not found!");
        }

        return booking;
    }
}
