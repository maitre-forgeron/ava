namespace Ava.Domain.Models.Booking
{
    public class Booking : AggregateRoot
    {
        public Guid ConsumerId { get; private set; }
        public Guid TherapistId { get; private set; }
        public DateTime PreferredTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime? StatusChangeTime { get; private set; }

        public Booking()
        {

        }

        public Booking(Guid id, Guid consumerId, Guid therapistId, DateTime preferredTime, TimeSpan duration) : base(id)
        {
            ConsumerId = consumerId;
            TherapistId = therapistId;
            PreferredTime = preferredTime;
            Duration = duration;
            Status = BookingStatus.InProgress;
        }

        public void Approve(Guid therapistId)
        {
            Status = BookingStatus.Accepted;
            StatusChangeTime = DateTime.UtcNow;
        }

        public void Reject(Guid therapistId)
        {
            Status = BookingStatus.Rejected;
            StatusChangeTime = DateTime.UtcNow;
        }

        private void EnsureTherapistAuthorization(Guid therapistId)
        {
            if (therapistId != TherapistId)
            {
                throw new UnauthorizedAccessException("Only the assigned therapist can approve or reject this booking");
            }
        }
    }

    public enum BookingStatus
    {
        InProgress,
        Accepted,
        Rejected
    }
}
