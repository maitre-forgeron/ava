using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Therapists.Queries
{
    public class GetTopReviewsForTherapistQuery : IRequest<List<ReviewDto>>
    {
        public Guid TherapistId { get; set; }
    }

    public class GetTopReviewsForTherapistQueryHandler : IRequestHandler<GetTopReviewsForTherapistQuery, List<ReviewDto>>
    {
        private readonly ITherapistRepository _therapistRepository;

        public GetTopReviewsForTherapistQueryHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task<List<ReviewDto>> Handle(GetTopReviewsForTherapistQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _therapistRepository.GetTopReviewsForTherapistAsync(request.TherapistId, 3);
            return reviews.Select(r => new ReviewDto
            {
                AuthorId = r.AuthorId,
                RecipientId = r.RecipientId,
                Rating = r.Rating,
                Summary = r.Summary
            }).ToList();
        }
    }
}
