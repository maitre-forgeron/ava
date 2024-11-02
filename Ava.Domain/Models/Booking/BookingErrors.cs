using Ava.Domain.Models.Common;

namespace Ava.Domain.Models.Booking
{
    public static class BookingErrors
    {
        public static readonly Error NotFound = new Error("Booking.NotFound", "Booking was not found!");
        public static readonly Error Unauthorized = new Error("Booking.Unauthorized", "Only the therapist can approve or reject this booking");
    }
}
