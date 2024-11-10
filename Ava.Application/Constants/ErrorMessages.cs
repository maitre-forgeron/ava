using Ava.Application.Models;

namespace Ava.Application.Constants;

public static class BookingErrors
{
    public static readonly Error NotFound = new Error("Booking.NotFound", "Booking was not found!");
    public static readonly Error Unauthorized = new Error("Booking.Unauthorized", "Only the therapist can approve or reject this booking");
}

public static class TherapistErrors
{
    public static readonly Error NotFound = new Error("Therapist.NotFound", "Therapist was not found!");
}

public static class CustomerErrors
{
    public static readonly Error NotFound = new Error("Customer.NotFound", "Customer was not found!");
}