using Ava.Application.Dtos;
using Ava.Application.Models;
using Ava.Domain.Models.Booking;
using Ava.Infrastructure.Db;

using MediatR;

namespace Ava.Application.Bookings.Commands;

public record AddBookingCommand(CreateBookingDto Dto) : IRequest<Result>;

public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, Result>
{
    private readonly AvaDbContext _context;

    public AddBookingCommandHandler(AvaDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AddBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = new Booking(Guid.NewGuid(), request.Dto.ConsumerId, request.Dto.TherapistId, request.Dto.PreferredTime, request.Dto.Duration);

        _context.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
