using Ava.Domain.Models.Booking;

namespace Ava.Application.Dtos;


public record BookingDto(Guid Id, Guid ConsumerId, Guid TherapistId, DateTime PreferredTime, TimeSpan Duration, BookingStatus Status, DateTime? StatusChangeTime);
public record CreateBookingDto(Guid Id, Guid ConsumerId, Guid TherapistId, DateTime PreferredTime, TimeSpan Duration);
public record BookingActionDto(Guid Id, Guid TherapistId);


