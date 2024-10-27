using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetTopReviewsForTherapistQuery : IRequest<List<ReviewDto>>
    {
        public Guid TherapistId { get; set; }
    }
}
