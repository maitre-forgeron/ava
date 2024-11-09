namespace Ava.Domain.Models.Booking
{
    public class Booking : AggregateRoot
    {
        public Guid ConsumerId { get; private set; }
        public Guid TherapistId { get; private set; }
        public DateTime PreferredTime { get; private set; }
        public int Duration { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime? StatusChangeTime { get; private set; }

        public Booking()
        {

        }

        public Booking(Guid id, Guid consumerId, Guid therapistId, DateTime preferredTime, int duration) : base(id)
        {
            ConsumerId = consumerId;
            TherapistId = therapistId;
            PreferredTime = preferredTime;
            Duration = duration;
            Status = BookingStatus.InProgress;
        }

        public void Approve()
        {
            Status = BookingStatus.Accepted;
            StatusChangeTime = DateTime.UtcNow;
        }

        public void Reject()
        {
            Status = BookingStatus.Rejected;
            StatusChangeTime = DateTime.UtcNow;
        }
    }

    public enum BookingStatus
    {
        InProgress,
        Accepted,
        Rejected
    }
}
