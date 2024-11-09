using Ava.Domain.Models.Booking;

namespace Ava.Application.Dtos;


public record BookingDto(Guid Id, Guid ConsumerId, Guid TherapistId, DateTime PreferredTime, int Duration, BookingStatus Status, DateTime? StatusChangeTime);
public record CreateBookingDto(Guid ConsumerId, Guid TherapistId, DateTime PreferredTime, int Duration);


