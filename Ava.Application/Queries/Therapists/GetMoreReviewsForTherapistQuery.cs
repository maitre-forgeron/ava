using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetMoreReviewsForTherapistQuery : IRequest<List<ReviewDto>>
    {
        public Guid TherapistId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
