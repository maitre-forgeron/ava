using Ava.Application.Dtos;
using Ava.Domain.Models.Booking;
using Ava.Infrastructure.Db;

using MediatR;

namespace Ava.Application.Bookings.Commands;

public record AddBookingCommand(CreateBookingDto Dto) : IRequest<Booking>;

public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, Booking>
{
    private readonly AvaDbContext _context;

    public AddBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> Handle(AddBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = new Booking(request.Dto.Id, request.Dto.ConsumerId, request.Dto.TherapistId, request.Dto.PreferredTime, request.Dto.Duration);

        _context.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return booking;
    }
}
