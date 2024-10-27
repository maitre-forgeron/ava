using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetTopReviewsForTherapistQuery : IRequest<List<Review>>
    {
        public Guid TherapistId { get; set; }
    }
}
