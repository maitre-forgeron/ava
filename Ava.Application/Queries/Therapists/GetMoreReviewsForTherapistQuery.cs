using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetMoreReviewsForTherapistQuery : IRequest<List<Review>>
    {
        public Guid TherapistId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
