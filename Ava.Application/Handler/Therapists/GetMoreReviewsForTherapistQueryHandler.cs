using Ava.Application.Dtos;
using Ava.Application.Queries.Therapists;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using MediatR;

namespace Ava.Application.Handler.Therapists
{
    public class GetMoreReviewsForTherapistQueryHandler : IRequestHandler<GetMoreReviewsForTherapistQuery, List<ReviewDto>>
    {
        private readonly ITherapistRepository _therapistRepository;

        public GetMoreReviewsForTherapistQueryHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task<List<ReviewDto>> Handle(GetMoreReviewsForTherapistQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _therapistRepository.GetReviewsForTherapistAsync(request.TherapistId, request.Skip, request.Take);
            return reviews.Select(r => new ReviewDto
            {
                SenderId = r.SenderId,
                RecipientId = r.RecipientId,
                ReviewValue = r.ReviewValue,
                ReviewText = r.ReviewText
            }).ToList();
        }
    }
}
